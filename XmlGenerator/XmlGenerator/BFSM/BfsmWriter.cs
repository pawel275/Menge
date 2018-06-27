using System;

namespace XmlGenerator.BFSM
{
    public class BfsmWriter : Writer, IDisposable
    {
        public GoalSet GoalSet { get; private set; }

        public State State { get; private set; }

        public Transition Transition { get; private set; }

        public BfsmWriter(string path) : base(path)
        {
            GoalSet = new GoalSet(xml);
            State = new State(xml);
            Transition = new Transition(xml);

            xml.WriteStartDocument();
            xml.WriteStartElement("BFSM");
        }

        public void Dispose()
        {
            xml.WriteEndElement();
            xml.Dispose();
        }
    }
}