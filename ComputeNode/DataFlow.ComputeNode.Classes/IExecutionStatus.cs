using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Provides methods for getting and setting execution core status.
    /// </summary>
    public interface IExecutionStatus
    {
        /// <summary>
        /// Gets and sets status.
        /// </summary>
        ExecutionStatus Status { get; set; }

        /// <summary>
        /// Gets and sets status message.
        /// </summary>
        string StatusMessage { get; set; }

        /// <summary>
        /// Gets and sets and instance of <see cref="Task"/> that is being processed currently.
        /// </summary>
        Task CurrentTask { get; set; }

        /// <summary>
        /// This event is raised when execution core status is changed.
        /// </summary>
        event EventHandler<ExecutionStatusChangedEventArgs> OnStatusChanged;

        /// <summary>
        /// This event is raised when execution core status message is changed.
        /// </summary>
        event EventHandler<ExecutionStatusMessageChangedEventArgs> OnStatusMessageChanged;
    }
}
