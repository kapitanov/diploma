using System;
using System.Collections.Generic;

namespace AISTek.DataFlow.Shared.Japi
{
    [Serializable]
    public class Job
    {
        public Job()
        {
            Name = string.Empty;
            Tasks = new List<Task>();
            Files = new List<File>();
        }

        public string Name { get; set; }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<Task> Tasks { get; private set; }

        public List<File> Files { get; set; }

        public override string ToString()
        {
            return string.Format("(\"{0}\", {{{1}}})", Name, Id);
        }

        [NonSerialized]
        private Guid id;
    }
}
