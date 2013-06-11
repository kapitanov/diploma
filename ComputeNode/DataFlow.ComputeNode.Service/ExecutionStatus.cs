using System;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Service
{
    public sealed class ExecutionStatus : IExecutionStatus
    {
        public ExecutionStatus()
        {
            StatusMessage = "";
        }

        #region Implementation of IExecutionStatus

        /// <summary>
        /// Gets and sets status.
        /// </summary>
        public Classes.ExecutionStatus Status
        {
            get { return status; }
            set
            {
                if (status == value)
                    return;
                status = value;
                InvokeOnStatusChanged(value);
            }
        }

        /// <summary>
        /// Gets and sets status message.
        /// </summary>
        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                if (statusMessage == value)
                    return;
                statusMessage = value;
                InvokeOnStatusMessageChanged(value);
            }
        }

        /// <summary>
        /// Gets and sets and instance of <see cref="Task"/> that is being processed currently.
        /// </summary>
        public Task CurrentTask { get; set; }

        /// <summary>
        /// This event is raised when execution core status is changed.
        /// </summary>
        public event EventHandler<ExecutionStatusChangedEventArgs> OnStatusChanged;

        /// <summary>
        /// This event is raised when execution core status message is changed.
        /// </summary>
        public event EventHandler<ExecutionStatusMessageChangedEventArgs> OnStatusMessageChanged;

        #endregion

        private Classes.ExecutionStatus status;
        private string statusMessage;

        private void InvokeOnStatusChanged(Classes.ExecutionStatus newStatus)
        {
            var handler = OnStatusChanged;
            if (handler != null)
                handler(this, new ExecutionStatusChangedEventArgs(newStatus));
        }

        private void InvokeOnStatusMessageChanged(string newStatusMessage)
        {
            var handler = OnStatusMessageChanged;
            if (handler != null)
                handler(this, new ExecutionStatusMessageChangedEventArgs(newStatusMessage));
        }
    }
}
