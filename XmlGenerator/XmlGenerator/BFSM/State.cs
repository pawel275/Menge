using System;
using System.Xml;

namespace XmlGenerator.BFSM
{
    public class State : Writer
    {
        public State(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void GoToGoal(string name, string goalSelector, int? goalSet = null, bool final = false)
        {
            _WriteState(() =>
            {
                xml.WriteStartElement("VelComponent");
                xml.WriteAttributeString("type", "goal");
                xml.WriteEndElement();
            }, name, goalSelector, goalSet, final);
        }

        public void GoToGoalWithMap(string name, string goalSelector, string fileName, int? goalSet = null, bool final = false)
        {
            _WriteState(() =>
            {
                xml.WriteStartElement("VelComponent");
                xml.WriteAttributeString("type", "road_map");
                xml.WriteAttributeString("file_name", fileName);
                xml.WriteEndElement();
            }, name, goalSelector, goalSet, final);
        }

        private void _WriteState(Action body, string name, string goalSelector, int? goalSet = null, bool final = false)
        {
            xml.WriteStartElement("State");
            xml.WriteAttributeString("name", name);
            xml.WriteAttributeString("final", final ? "1" : "0");

            xml.WriteStartElement("GoalSelector");
            xml.WriteAttributeString("type", goalSelector);
            if (goalSet.HasValue)
            {
                xml.WriteAttributeString("goal_set", goalSet.Value.ToString());
            }
            xml.WriteEndElement();

            body();

            xml.WriteEndElement();
        }
    }
}