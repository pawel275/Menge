using System;
using System.Xml;

namespace XmlGenerator.BFSM
{
    public class Transition : Writer
    {
        public Transition(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void Simple(string from, string to, string condition)
        {
            _WriteTransition(from, to, () =>
            {
                xml.WriteStartElement("Condition");
                xml.WriteAttributeString("type", condition);
                xml.WriteEndElement();
            });
        }

        public void AABB(string from, string to, (double min_x, double min_y, double max_x, double max_y) position, bool inside)
        {
            _WriteTransition(from, to, () =>
            {
                xml.WriteStartElement("Condition");
                xml.WriteAttributeString("type", "AABB");
                xml.WriteAttributeString("inside", inside ? "1" : "0");
                xml.WriteAttributeString("min_x", Utils.Str(position.min_x));
                xml.WriteAttributeString("min_y", Utils.Str(position.min_y));
                xml.WriteAttributeString("max_x", Utils.Str(position.max_x));
                xml.WriteAttributeString("max_y", Utils.Str(position.max_y));
                xml.WriteEndElement();
            });
        }

        private void _WriteTransition(string from, string to, Action body)
        {
            xml.WriteStartElement("Transition");
            xml.WriteAttributeString("from", from);
            xml.WriteAttributeString("to", to);
            body();
            xml.WriteEndElement();
        }
    }
}