using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes.Exceptions
{
    /// <summary>
    /// This exception is thrown when a  task scheduler gets an unexpected task.
    /// </summary>
    [Serializable]
    public class WrongTaskException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="WrongTaskException"/> class.
        /// </summary>
        public WrongTaskException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="WrongTaskException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public WrongTaskException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="WrongTaskException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="taskId">
        /// Unique identifier of task that caused this exception. 
        /// </param>
        public WrongTaskException(string message, Guid taskId)
            : base(message)
        {
            TaskId = taskId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="WrongTaskException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public WrongTaskException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="WrongTaskException"/> class.
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
        public WrongTaskException(string message, Guid taskId, Exception inner)
            : base(message, inner)
        {
            TaskId = taskId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="WrongTaskException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected WrongTaskException(
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


