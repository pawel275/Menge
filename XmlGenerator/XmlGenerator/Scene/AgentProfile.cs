using System;
using System.Xml;

namespace XmlGenerator.Scene
{
    public class AgentProfile : Writer
    {
        public AgentProfile(XmlWriter xmlWriter) : base(xmlWriter)
        {
        }

        public void Default(
            string name,
            string @class,
            string obstacleSet,
            string max_angle_vel = "360",
            string max_neighbors = "10",
            string neighbor_dist = "5",
            string r = "0.19",
            string pref_speed = "1.04",
            string max_speed = "2",
            string max_accel = "5",
            string tau = "3.0",
            string tauObst = "0.15")
        {
            _WriteProfile(name, () =>
            {
                xml.WriteStartElement("Common");
                xml.WriteAttributeString("max_angle_vel", max_angle_vel);
                xml.WriteAttributeString("max_neighbors", max_neighbors);
                xml.WriteAttributeString("obstacleSet", obstacleSet);
                xml.WriteAttributeString("neighbor_dist", neighbor_dist);
                xml.WriteAttributeString("r", r);
                xml.WriteAttributeString("class", @class);
                xml.WriteAttributeString("pref_speed", pref_speed);
                xml.WriteAttributeString("max_speed", max_speed);
                xml.WriteAttributeString("max_accel", max_accel);
                xml.WriteEndElement();

                xml.WriteStartElement("ORCA");
                xml.WriteAttributeString("tau", tau);
                xml.WriteAttributeString("tauObst", tauObst);
                xml.WriteEndElement();
            });
        }

        private void _WriteProfile(string name, Action body)
        {
            xml.WriteStartElement("AgentProfile");
            xml.WriteAttributeString("name", name);
            body();
            xml.WriteEndElement();
        }
    }
}