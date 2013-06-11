using System;
using AISTek.DataFlow.MasterNode.DataModel;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// Provides data for <see cref="ITaskQueue"/> events.
    /// </summary>
    public class TaskEventArgs : EventArgs
    {
        /// <summary>
        /// Initalizes new instance of <see cref="TaskEventArgs"/> class.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that provides data for current event.
        /// </param>
        public TaskEventArgs(Task task)
        {
            Task = task;
        }

        /// <summary>
        /// Gets an instance of <see cref="Task"/> that provides data for current event.
        /// </summary>
        public Task Task { get; private set; }
    }
}
