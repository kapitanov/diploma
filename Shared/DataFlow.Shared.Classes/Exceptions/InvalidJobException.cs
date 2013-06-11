using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes.Exceptions
{
    /// <summary>
    /// This exception is thrown when a job fails to be started due to its content.
    /// </summary>
    [Serializable]
    public class InvalidJobException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="InvalidJobException"/> class.
        /// </summary>
        public InvalidJobException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="InvalidJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public InvalidJobException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="InvalidJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="jobId">
        /// Unique identifier of job that caused this exception.
        /// </param>
        public InvalidJobException(string message, Guid jobId)
            : base(message)
        {
            JobId = jobId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="InvalidJobException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public InvalidJobException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="InvalidJobException"/> class.
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
        public InvalidJobException(string message, Guid jobId, Exception inner)
            : base(message, inner)
        {
            JobId = jobId;
        }

        /// <summary>
        /// Initializes new instance of <see cref="InvalidJobException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected InvalidJobException(
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


