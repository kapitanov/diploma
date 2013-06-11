using System;
using System.Xml.Serialization;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    [Serializable]
    public abstract class FileRequest
    {
        public string Name { get; set; }

        [XmlIgnore]
        public FileLink Link
        {
            get { return link; }
            protected set { link = value; }
        }

        public abstract FileLink SaveFile(IRepository repository);

        public override string ToString()
        {
            return Name;
        }

        [NonSerialized]
        private FileLink link;
    }
}
