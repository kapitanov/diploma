using System;
using System.Runtime.Serialization;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// This exception is thrown when a task execution engine rejects a task.
    /// </summary>
    [Serializable]
    public class TaskRejectedException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="TaskRejectedException"/> class.
        /// </summary>
        public TaskRejectedException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TaskRejectedException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public TaskRejectedException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TaskRejectedException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="task">
        /// An instance of <see cref="Shared.Classes.Task"/> that caused this exception. 
        /// </param>
        public TaskRejectedException(string message, Task task)
            : base(message)
        {
            Task = task;
        }

        /// <summary>
        /// Initializes new instance of <see cref="TaskRejectedException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public TaskRejectedException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="TaskRejectedException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="task">
        /// An instance of <see cref="Shared.Classes.Task"/> that caused this exception.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public TaskRejectedException(string message, Task task, Exception inner)
            : base(message, inner)
        {
            Task = task;
        }

        /// <summary>
        /// Initializes new instance of <see cref="TaskRejectedException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected TaskRejectedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// An UID of <see cref="Shared.Classes.Task"/> that caused current exception.
        /// </summary>
        public Task Task { get; private set; }
    }
}
