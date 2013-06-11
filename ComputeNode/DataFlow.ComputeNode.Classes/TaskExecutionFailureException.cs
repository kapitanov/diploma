using System;
using System.Runtime.Serialization;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// This exception is thrown when task execution engine fails to execute the task due to internal reasons.
    /// </summary>
    [Serializable]
    public class TaskExecutionFailureException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="TaskExecutionFailureException"/> class.
        /// </summary>
        public TaskExecutionFailureException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TaskExecutionFailureException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public TaskExecutionFailureException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TaskExecutionFailureException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="taskId">
        /// Unique identifier of task that caused this exception. 
        /// </param>
        public TaskExecutionFailureException(string message, Guid taskId)
            : base(message)
        {
            TaskId = taskId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="TaskExecutionFailureException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public TaskExecutionFailureException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TaskExecutionFailureException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="taskId">
        /// Unique identifier of task that caused this exception. 
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public TaskExecutionFailureException(string message, Guid taskId, Exception inner)
            : base(message, inner)
        {
            TaskId = taskId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="TaskExecutionFailureException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected TaskExecutionFailureException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// An UID of <see cref="Task"/> that caused current exception.
        /// </summary>
        public Guid TaskId { get; private set; }
    }
}
