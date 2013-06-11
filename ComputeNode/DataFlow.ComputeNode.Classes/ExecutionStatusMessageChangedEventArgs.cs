using System;

namespace AISTek.DataFlow.ComputeNode.Classes
{
       /// <summary>
    /// Provides data for <see cref="IExecutionStatus"/>.
    /// </summary>
    public class ExecutionStatusMessageChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of <see cref="ExecutionStatusMessageChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newStatusMessage">
        /// A value of <see cref="ExecutionStatus"/> that defines new status.
        /// </param>
        public ExecutionStatusMessageChangedEventArgs(string newStatusMessage)
        {
            NewStatusMessage = newStatusMessage;
        }

        /// <summary>
        /// Gets new status.
        /// </summary>
        public string NewStatusMessage { get; private set; }
    }
}
