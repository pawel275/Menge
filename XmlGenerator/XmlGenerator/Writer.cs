using System.Xml;

namespace XmlGenerator
{
    public abstract class Writer
    {
        private static XmlWriterSettings XmlSettings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t"
        };

        protected XmlWriter xml;

        public Writer(string path)
        {
            xml = XmlWriter.Create(path, XmlSettings);
        }

        public Writer(XmlWriter xmlWriter)
        {
            xml = xmlWriter;
        }
    }
}