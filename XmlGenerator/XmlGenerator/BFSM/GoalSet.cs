using System;
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
            xml.WriteStartElement("GoalSet");
            xml.WriteAttributeString("id", id);

            for (int i = 0; i < count; i++)
            {
                xml.WriteStartElement("Goal");
                xml.WriteAttributeString("type", "evAABB");
                xml.WriteAttributeString("id", i.ToString());
                double x = Math.Cos(i * 2 * Math.PI / count) * radius;
                double y = Math.Sin(i * 2 * Math.PI / count) * radius;
                xml.WriteAttributeString("min_x", Utils.Str(x - size / 2));
                xml.WriteAttributeString("min_y", Utils.Str(y - size / 2));
                xml.WriteAttributeString("max_x", Utils.Str(x + size / 2));
                xml.WriteAttributeString("max_y", Utils.Str(y + size / 2));
                xml.WriteAttributeString("weight", "1.0");
                xml.WriteAttributeString("adjacent", $"{(i + count - 1) % count},{(i + 1) % count}");
                xml.WriteEndElement();
            }

            xml.WriteEndElement();
        }
    }
}