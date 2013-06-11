using System;
using QuickGraph;

namespace AISTek.DataFlow.Tools.JobEditor.ViewModel
{
    [Serializable]
    public class TaskGraph : BidirectionalGraph<TaskVertex, TaskEdge>
    {
        public TaskGraph()
            : base(false)
        { }
    }
}
