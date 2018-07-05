using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace XmlGenerator.Scene
{
    public class WallsParser
    {
        private XDocument xml;

        public WallsParser(string filePath)
        {
            xml = XDocument.Load(filePath);
        }

        public IEnumerable<(double x1, double y1, double x2, double y2)> ParseLines(double scale = 1, double dx = 0, double dy = 0)
        {
            var g = xml.Root.Elements().First(x => x.Name.LocalName == "g");
            foreach (var line in g.Elements())
            {
                var attributes = line.Attributes();

                double x1 = (double.Parse(attributes.First(x => x.Name.LocalName == "x1").Value) + dx) * scale;
                double y1 = (double.Parse(attributes.First(x => x.Name.LocalName == "y1").Value) + dy) * scale;
                double x2 = (double.Parse(attributes.First(x => x.Name.LocalName == "x2").Value) + dx) * scale;
                double y2 = (double.Parse(attributes.First(x => x.Name.LocalName == "y2").Value) + dy) * scale;

                yield return (x1, y1, x2, y2);
            }
        }
    }
}