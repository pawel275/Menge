using System.Collections.Generic;
using System.Xml;

namespace XmlGenerator.Scene
{
    public class ObstacleSet : Writer
    {
        public ObstacleSet(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public IEnumerable<(double x1, double y1, double x2, double y2)> Parsed(string filePath, string @class, double scale = 1, double dx = 0, double dy = 0)
        {
            var parser = new WallsParser(filePath);
            var lines = parser.ParseLines();

            xml.WriteStartElement("ObstacleSet");
            xml.WriteAttributeString("type", "explicit");
            xml.WriteAttributeString("class", @class);

            foreach (var (x1, y1, x2, y2) in lines)
            {
                _WriteLine(Utils.Str((x1 + dx) * scale), Utils.Str((y1 + dy) * scale), Utils.Str((x2 + dx) * scale), Utils.Str((y2 + dy) * scale));
            }

            xml.WriteEndElement();

            return lines;
        }

        private void _WriteLine(string x1, string y1, string x2, string y2)
        {
            xml.WriteStartElement("Obstacle");
            xml.WriteAttributeString("closed", "1");
            _WriteVertex(x1, y1);
            _WriteVertex(x2, y2);
            xml.WriteEndElement();
        }

        private void _WriteVertex(string p_x, string p_y)
        {
            xml.WriteStartElement("Vertex");
            xml.WriteAttributeString("p_x", p_x);
            xml.WriteAttributeString("p_y", p_y);
            xml.WriteEndElement();
        }
    }
}