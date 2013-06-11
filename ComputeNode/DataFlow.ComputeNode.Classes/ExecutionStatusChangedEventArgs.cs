using System;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Provides data for <see cref="IExecutionStatus"/>.
    /// </summary>
    public class ExecutionStatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of <see cref="ExecutionStatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newStatus">
        /// A value of <see cref="ExecutionStatus"/> that defines new status.
        /// </param>
        public ExecutionStatusChangedEventArgs (ExecutionStatus newStatus)
        {
            NewStatus = newStatus;
        }

        /// <summary>
        /// Gets new status.
        /// </summary>
        public ExecutionStatus NewStatus { get; private set; }
    }
}
