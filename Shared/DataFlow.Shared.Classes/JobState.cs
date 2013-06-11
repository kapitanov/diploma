namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Defines job's possible states.
    /// </summary>
    public enum JobState
    {
        /// <summary>
        /// Job is being created.
        /// </summary>
        Creating = 0,

        /// <summary>
        /// Job is created and ready for processing.
        /// </summary>
        Ready,

        /// <summary>
        /// Job is being processed.
        /// </summary>
        Processing,

        /// <summary>
        /// Job has being processed, but it was cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// Job has been processed successfully.
        /// </summary>
        Completed,

        /// <summary>
        /// At least one of current job's tasks failed.
        /// </summary>
        Failed,

        /// <summary>
        /// Job has been deleted from scheduler
        /// </summary>
        Deleted,

        /// <summary>
        /// Job with specified GUID doesn't exists
        /// </summary>
        NotExists = int.MaxValue
    }
}
