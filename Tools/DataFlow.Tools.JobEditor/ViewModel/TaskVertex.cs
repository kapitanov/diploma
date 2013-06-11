using System;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Tools.JobEditor.DataModel;

namespace AISTek.DataFlow.Tools.JobEditor.ViewModel
{
    [Serializable]
    public class TaskVertex 
    {
        public TaskVertex(TaskRequest task)
        {
            Task = task;
        }

        public TaskVertex ()
            : this(new TaskRequest())
        { }

        public TaskRequest Task { get; set; }

        public string Name
        {
            get
            {
                return Task != null ? Task.ToString() : "<NoTask>";
            }
        }
    }
}
