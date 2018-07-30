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

        public void Circle(int id, int count, double radius = 5, double size = 1)
        {
            using (var writer = Explicit(id))
            {
                for (int i = 0; i < count; i++)
                {
                    double x = Math.Cos(i * 2 * Math.PI / count) * radius;
                    double y = Math.Sin(i * 2 * Math.PI / count) * radius;

                    writer.Goal(i, x, y, new[] { (i + count - 1) % count, (i + 1) % count }, (i + 1) % count, size);
                }
            }
        }

        public void Parsed(List<Goal> paths, int id, double size = 1, double scale = 1, double dx = 0, double dy = 0)
        {
            HashSet<int> writtenGoals = new HashSet<int>();

            using (var writer = Explicit(id))
            {
                void writePath(Goal path, Goal excluded = null)
                {
                    if (!writtenGoals.Contains(path.Id))
                    {
                        writer.Goal(path.Id, path.X + dx, path.Y + dy, path.Adjacent.Select(x => x.Id), path.Next?.Id, size, scale);
                    }
                    writtenGoals.Add(path.Id);
                    foreach (var adj in path.Adjacent.Where(x => !writtenGoals.Contains(x.Id)))
                    {
                        writePath(adj);
                    }
                }

                foreach (var path in paths)
                {
                    writePath(path);
                }
            }
        }

        public GoalSetWriter Explicit(int id)
        {
            return new GoalSetWriter(xml, id.ToString());
        }

        public class GoalSetWriter : Writer, IDisposable
        {
            public GoalSetWriter(XmlWriter xmlWriter, string id) : base(xmlWriter)
            {
                xml.WriteStartElement("GoalSet");
                xml.WriteAttributeString("id", id);
            }

            public void Goal(int id, double x, double y, IEnumerable<int> adjacent = null, int? next = null, double size = 1, double scale = 1)
            {
                _WriteEvAABBGoal(
                    id: id,
                    position: Utils.BoxPosition(x, y, size),
                    weight: "1.0",
                    adjacent: adjacent == null ? string.Empty : string.Join(",", adjacent),
                    next: next.HasValue ? next.Value.ToString() : string.Empty,
                    scale: scale);
            }

            private void _WriteEvAABBGoal(
            int id,
            (double min_x, double min_y, double max_x, double max_y) position,
            string weight,
            string adjacent,
            string next,
            double scale = 1D)
            {
                xml.WriteStartElement("Goal");
                xml.WriteAttributeString("type", "evAABB");
                xml.WriteAttributeString("id", id.ToString());
                xml.WriteAttributeString("min_x", Utils.Str(position.min_x * scale));
                xml.WriteAttributeString("min_y", Utils.Str(position.min_y * scale));
                xml.WriteAttributeString("max_x", Utils.Str(position.max_x * scale));
                xml.WriteAttributeString("max_y", Utils.Str(position.max_y * scale));
                xml.WriteAttributeString("weight", weight);
                xml.WriteAttributeString("adjacent", adjacent);
                xml.WriteAttributeString("next", next);
                xml.WriteEndElement();
            }

            public void Dispose()
            {
                xml.WriteEndElement();
            }
        }
    }
}