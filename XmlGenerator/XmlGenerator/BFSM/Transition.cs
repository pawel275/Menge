using System.Xml;

namespace XmlGenerator.BFSM
{
    public class Transition : Writer
    {
        public Transition(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void SimpleContition(string from, string to, string condition)
        {
            xml.WriteStartElement("Transition");
            xml.WriteAttributeString("from", from);
            xml.WriteAttributeString("to", to);

            xml.WriteStartElement("Condition");
            xml.WriteAttributeString("type", condition);
            xml.WriteEndElement();

            xml.WriteEndElement();
        }
    }
}