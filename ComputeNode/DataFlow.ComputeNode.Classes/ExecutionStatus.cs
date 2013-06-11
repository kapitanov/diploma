namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Defines possible statuses for execution engine.
    /// </summary>
    public enum ExecutionStatus
    {
        /// <summary>
        /// Engine is waiting for task.
        /// </summary>
        WaitingForTask,

        /// <summary>
        /// Engine is processing the task
        /// </summary>
        ProcessingTask
    }
}
