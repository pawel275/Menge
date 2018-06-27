using System.Xml;

namespace XmlGenerator.BFSM
{
    public class State : Writer
    {
        public State(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void GoToGoal(string name, string goalSelector, string goalSet, bool final = false)
        {
            xml.WriteStartElement("State");
            xml.WriteAttributeString("name", name);
            xml.WriteAttributeString("final", final ? "1" : "0");

            xml.WriteStartElement("GoalSelector");
            xml.WriteAttributeString("type", goalSelector);
            xml.WriteAttributeString("goal_set", goalSet);
            xml.WriteEndElement();

            xml.WriteStartElement("VelComponent");
            xml.WriteAttributeString("type", "goal");
            xml.WriteEndElement();

            xml.WriteEndElement();
        }
    }
}