namespace AISTek.DataFlow.MasterNode.DataModel
{
    /// <summary>
    /// Defines task's possible states.
    /// </summary>
    public enum TaskState
    {
        /// <summary>
        /// Task created, but it hasn't been added into a job.
        /// </summary>
        Unattached,

        /// <summary>
        /// Task's been created and added into a job, but it hasn't been started.
        /// </summary>
        Unavailible,

        /// <summary>
        /// Task's been created, added into a job and all its inputs are ready.
        /// Task can be taken for processing.
        /// </summary>
        NotReady,

        /// <summary>
        /// Task's been created, added into a job and all its inputs are ready.
        /// Task can be taken for processing.
        /// </summary>
        Pending,

        /// <summary>
        /// Task which was in <see cref="Pending"/> state
        /// has been taken for processing.
        /// </summary>
        Processing,

        /// <summary>
        /// Task has been cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// Task which has been taken for processing
        /// returned a result and its result is stored in scheduler.
        /// </summary>
        Completed,

        /// <summary>
        /// Task which has been taken for processing
        /// throwed an exception. 
        /// Task in this state will not be processed 
        /// until all job'd be restarted explicitly.
        /// </summary>
        Failed,

        /// <summary>
        /// Task has been removed from task manager.
        /// </summary>
        Deleted
    }
}
