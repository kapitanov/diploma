using System;
using QuickGraph;

namespace AISTek.DataFlow.Tools.JobEditor.ViewModel
{
    [Serializable]
    public class TaskEdge : Edge<TaskVertex>
    {
        public TaskEdge(TaskVertex source, TaskVertex target)
            : base(source, target)
        { }
    }
}
