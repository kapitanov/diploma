using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Tools.JobEditor.ViewModel;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    [Serializable]
    public class JobDefinition
    {
        public JobDefinition()
        {
            Name = string.Empty;
            Tasks = new List<TaskRequest>();
            Files = new List<FileRequest>();
            Graph = new TaskGraph();
        }

        public string Name { get; set; }

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public TaskGraph Graph
        {
            get { return graph; }
            set { graph = value; }
        }

        public List<TaskRequest> Tasks { get; private set; }

        public List<FileRequest> Files { get; set; }

        public override string ToString()
        {
            return string.Format("(\"{0}\", {{{1}}})", Name, Id);
        }

        [NonSerialized]
        private TaskGraph graph;

        [NonSerialized]
        private Guid id;
    }
}
