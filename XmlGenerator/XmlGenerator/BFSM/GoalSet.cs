using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace XmlGenerator.BFSM
{
    public class GoalSet : Writer
    {
        public GoalSet(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void Circle(string id, int count, double radius = 5, double size = 1)
        {
            _WriteGoalSet(id, () =>
            {
                for (int i = 0; i < count; i++)
                {
                    double x = Math.Cos(i * 2 * Math.PI / count) * radius;
                    double y = Math.Sin(i * 2 * Math.PI / count) * radius;
                    _WriteEvAABBGoal(
                        id: i,
                        position: _BoxPosition(x, y, size),
                        weight: "1.0",
                        adjacent: $"{(i + count - 1) % count},{(i + 1) % count}",
                        next: ((i + 1) % count).ToString());
                }
            });
        }

        public void Parsed(string filePath, string id, double size = 1)
        {
            var parser = new GoalParser(filePath);
            var paths = parser.ParsePaths();

            HashSet<int> writtenGoals = new HashSet<int>();

            void writePath(Goal path, Goal excluded = null)
            {
                writtenGoals.Add(path.Id);
                _WriteEvAABBGoal(
                    id: path.Id,
                    position: _BoxPosition(path.X, path.Y, size),
                    weight: "1.0",
                    adjacent: string.Join(",", path.Adjacent.Select(x => x.Id)),
                    next: path.Next?.Id.ToString() ?? string.Empty);
                foreach (var adj in path.Adjacent.Where(x => !writtenGoals.Contains(x.Id)))
                {
                    writePath(adj);
                }
            }

            _WriteGoalSet(id, () =>
            {
                foreach (var path in paths)
                {
                    writePath(path);
                }
            });
        }

        private void _WriteGoalSet(string id, Action body)
        {
            xml.WriteStartElement("GoalSet");
            xml.WriteAttributeString("id", id);
            body();
            xml.WriteEndElement();
        }

        private void _WriteEvAABBGoal(
            int id,
            (double min_x, double min_y, double max_x, double max_y) position,
            string weight,
            string adjacent,
            string next)
        {
            xml.WriteStartElement("Goal");
            xml.WriteAttributeString("type", "evAABB");
            xml.WriteAttributeString("id", id.ToString());
            xml.WriteAttributeString("min_x", Utils.Str(position.min_x));
            xml.WriteAttributeString("min_y", Utils.Str(position.min_y));
            xml.WriteAttributeString("max_x", Utils.Str(position.max_x));
            xml.WriteAttributeString("max_y", Utils.Str(position.max_y));
            xml.WriteAttributeString("weight", weight);
            xml.WriteAttributeString("adjacent", adjacent);
            xml.WriteAttributeString("next", next);
            xml.WriteEndElement();
        }

        private (double, double, double, double) _BoxPosition(double x, double y, double size)
        {
            return (
                x - size / 2,
                y - size / 2,
                x + size / 2,
                y + size / 2);
        }
    }
}