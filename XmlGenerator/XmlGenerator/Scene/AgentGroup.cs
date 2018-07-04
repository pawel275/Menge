using System.Xml;

namespace XmlGenerator.Scene
{
    public class AgentGroup : Writer
    {
        public AgentGroup(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void Single(string p_x, string p_y, string profile, string state)
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

            xml.WriteStartElement("Generator");
            xml.WriteAttributeString("type", "explicit");
            xml.WriteStartElement("Agent");
            xml.WriteAttributeString("p_x", p_x);
            xml.WriteAttributeString("p_y", p_y);
            xml.WriteEndElement();
            xml.WriteEndElement();

            xml.WriteEndElement();
        }
    }
}