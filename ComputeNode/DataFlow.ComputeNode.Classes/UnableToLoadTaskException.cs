using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// This exception is thrown when a task execution engine is unable to load task files.
    /// </summary>
    [Serializable]
    public class UnableToLoadTaskException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="UnableToLoadTaskException"/> class.
        /// </summary>
        public UnableToLoadTaskException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="UnableToLoadTaskException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public UnableToLoadTaskException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="UnableToLoadTaskException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public UnableToLoadTaskException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="UnableToLoadTaskException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected UnableToLoadTaskException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }
    }
}


