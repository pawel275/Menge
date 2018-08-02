using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XmlGenerator.BFSM;

namespace XmlGenerator.Scene
{
    public class AgentGroup : Writer
    {
        public AgentGroup(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void Single(double p_x, double p_y, string profile, string state)
        {
            _WriteAgentGroup(profile, state, () =>
            {
                _WriteExplicitGenerator(() =>
                {
                    _WriteAgent(p_x, p_y);
                });
            });
        }

        public void Random(
            IEnumerable<(double x1, double y1, double x2, double y2)> walls,
            IEnumerable<Goal> goals,
            double scale,
            int count,
            string profile,
            string state)
        {
            var rnd = new Random();

            double inRange(double min, double max)
            {
                return min + (max - min) * rnd.NextDouble();
            }

            double minX = walls.Min(w => Math.Min(w.x1, w.x2));
            double maxX = walls.Max(w => Math.Max(w.x1, w.x2));
            double minY = walls.Min(w => Math.Min(w.y1, w.y2));
            double maxY = walls.Max(w => Math.Max(w.y1, w.y2));

            _WriteAgentGroup(profile, state, () =>
            {
                _WriteExplicitGenerator(() =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        double p_x = inRange(minX, maxX);
                        double p_y = inRange(minY, maxY);
                        _WriteAgent(p_x * scale, p_y * scale, FindGoal(p_x, p_y, walls, goals));
                    }
                });
            });
        }

        private int FindGoal(
            double p_x,
            double p_y,
            IEnumerable<(double x1, double y1, double x2, double y2)> walls,
            IEnumerable<Goal> goals)
        {
            bool isGoalVisible(Goal goal, (double x1, double y1, double x2, double y2) wall)
            {
                double a, x, y;
                bool result;

                a = Math.Tan(Math.Atan2(goal.Y - p_y, goal.X - p_x));

                if (wall.y1 == wall.y2)
                {
                    y = wall.y1;
                    x = (y - p_y) / a + p_x;
                    result = Math.Min(wall.x1, wall.x2) > x || Math.Max(wall.x1, wall.x2) < x;
                }
                else
                {
                    x = wall.x1;
                    y = a * (x - p_x) + p_y;
                    result = Math.Min(wall.y1, wall.y2) > y || Math.Max(wall.y1, wall.y2) < y;
                }

                return result || (x < Math.Min(p_x, goal.X) || x > Math.Max(p_x, goal.X));
            }

            var visibleGoals = goals.Where(g => walls.All(w => isGoalVisible(g, w)));

            return visibleGoals.OrderBy(g => Utils.Sqr(g.X - p_x) + Utils.Sqr(g.Y - p_y)).First().Id;
        }

        private void _WriteAgent(double p_x, double p_y, int goal = -1)
        {
            xml.WriteStartElement("Agent");
            xml.WriteAttributeString("p_x", Utils.Str(p_x));
            xml.WriteAttributeString("p_y", Utils.Str(p_y));
            xml.WriteAttributeString("goal", Utils.Str(goal));
            xml.WriteEndElement();
        }

        private void _WriteExplicitGenerator(Action body)
        {
            xml.WriteStartElement("Generator");
            xml.WriteAttributeString("type", "evacuation");
            body();
            xml.WriteEndElement();
        }

        private void _WriteAgentGroup(string profile, string state, Action body)
        {
            xml.WriteStartElement("AgentGroup");

            xml.WriteStartElement("ProfileSelector");
            xml.WriteAttributeString("type", "const");
            xml.WriteAttributeString("name", profile);
            xml.WriteEndElement();

            xml.WriteStartElement("StateSelector");
            xml.WriteAttributeString("type", "const");
            xml.WriteAttributeString("name", state);
            xml.WriteEndElement();

            body();

            xml.WriteEndElement();
        }
    }
}