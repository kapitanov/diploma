using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes.Exceptions
{
    /// <summary>
    /// This exception is thrown when a  task scheduler gets an unexpected task.
    /// </summary>
    [Serializable]
    public class WrongJobException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="WrongJobException"/> class.
        /// </summary>
        public WrongJobException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="WrongJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public WrongJobException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="WrongJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="jobId">
        /// Unique identifier of job that caused this exception.
        /// </param>
        public WrongJobException(string message, Guid jobId)
            : base(message)
        {
            JobId = jobId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="WrongJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public WrongJobException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="WrongJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="jobId">
        /// Unique identifier of job that caused this exception.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public WrongJobException(string message, Guid jobId, Exception inner)
            : base(message, inner)
        {
            JobId = jobId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="WrongJobException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected WrongJobException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// An UID of <see cref="Job"/> that caused current exception.
        /// </summary>
        public Guid JobId { get; private set; }
    }
}


