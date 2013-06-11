using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.MasterNode.Core
{
   /// <summary>
    /// This exception is thrown when core application gets unhandled critical exception.
    /// </summary>
    [Serializable]
    public class CoreApplicationException : ApplicationException
    {
        /// <summary>
        /// Initializes new instance of <see cref="CoreApplicationException"/> class.
        /// </summary>
        public CoreApplicationException()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="CoreApplicationException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        public CoreApplicationException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes new instance of <see cref="CoreApplicationException"/> class.
        /// </summary>
        /// <param name="message">
        /// Error message.
        /// </param>
        /// <param name="inner">
        /// Exception that causes this exception.
        /// </param>
        public CoreApplicationException(string message, Exception inner)
            : base(message, inner)
        { }
        
        /// <summary>
        /// Initializes new instance of <see cref="CoreApplicationException"/> class.
        /// </summary>
        /// <param name="info">
        /// Serialization information.
        /// </param>
        /// <param name="context">
        /// Streaming context.
        /// </param>
        protected CoreApplicationException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context)
        { }
    }
}
