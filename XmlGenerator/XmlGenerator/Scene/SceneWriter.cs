using System;

namespace XmlGenerator.Scene
{
    public class SceneWriter : Writer, IDisposable
    {
        public AgentProfile AgentProfile { get; private set; }

        public AgentGroup AgentGroup { get; private set; }

        public ObstacleSet ObstacleSet { get; private set; }

        public SceneWriter(string path) : base(path)
        {
            AgentProfile = new AgentProfile(xml);
            AgentGroup = new AgentGroup(xml);
            ObstacleSet = new ObstacleSet(xml);

            xml.WriteStartDocument();
            xml.WriteStartElement("Experiment");
            xml.WriteAttributeString("version", "2.0");
            _SceneSettings();
        }

        private void _SceneSettings()
        {
            xml.WriteStartElement("SpatialQuery");
            xml.WriteAttributeString("type", "kd-tree");
            xml.WriteAttributeString("test_visibility", "true");
            xml.WriteEndElement();

            xml.WriteStartElement("Common");
            xml.WriteAttributeString("time_step", "0.1");
            xml.WriteEndElement();
        }

        public void Dispose()
        {
            xml.WriteEndElement();
            xml.Dispose();
        }
    }
}