namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Represents the execution results state.
    /// </summary>
    public enum ExecutionResultState
    {
        /// <summary>
        /// Task has been executed successfully.
        /// </summary>
        Success,

        /// <summary>
        /// Task has been rejected.
        /// </summary>
        Rejected,

        /// <summary>
        /// Task failed to be executed.
        /// </summary>
        Failed
    }
}
