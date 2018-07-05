using System;
using System.Xml;

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

        public void RandomInArea(double minX, double minY, double maxX, double maxY, int count, string profile, string state)
        {
            var rnd = new Random();

            double fromRange(double min, double max)
            {
                return min + (max - min) * rnd.NextDouble();
            }

            _WriteAgentGroup(profile, state, () =>
            {
                _WriteExplicitGenerator(() =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        _WriteAgent(fromRange(minX, maxX), fromRange(minY, maxY));
                    }
                });
            });
        }

        private void _WriteAgent(double p_x, double p_y)
        {
            xml.WriteStartElement("Agent");
            xml.WriteAttributeString("p_x", Utils.Str(p_x));
            xml.WriteAttributeString("p_y", Utils.Str(p_y));
            xml.WriteEndElement();
        }

        private void _WriteExplicitGenerator(Action body)
        {
            xml.WriteStartElement("Generator");
            xml.WriteAttributeString("type", "explicit");
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