using System;
using AISTek.DataFlow.MasterNode.DataModel;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// Provides data for <see cref="IJobScheduler"/> events.
    /// </summary>
    public class JobEventArgs : EventArgs
    {
        /// <summary>
        /// Initalizes new instance of <see cref="JobEventArgs"/> class.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/> that provides data for current event.
        /// </param>
        public JobEventArgs(Job job)
        {
            Job = job;
        }

        /// <summary>
        /// Gets an instance of <see cref="Job"/> that provides data for current event.
        /// </summary>
        public Job Job { get; private set; }
    }
}
