using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Provides error report information.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    public class Error
    {
        /// <summary>
        /// Initializes new instance of <see cref="Error"/> class.
        /// </summary>
        public Error()
        {
            Message = string.Empty;
            Source = string.Empty;
            ExceptionType = string.Empty;
            StackTrace = string.Empty;
            InnerError = null;
        }

        /// <summary>
        /// Initializes new instance of <see cref="Error"/> class.
        /// </summary>
        /// <param name="exception">
        /// An instance of <see cref="Exception"/> class that contains error information. 
        /// </param>
        public Error(Exception exception)
        {
            Message = exception.Message;
            Source = exception.Source;
            ExceptionType = exception.GetType().FullName;
            StackTrace = exception.StackTrace;

            if (exception.InnerException != null)
                InnerError = new Error(exception);
        }

        /// <summary>
        /// Gets error message.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Message { get; set; }

        /// <summary>
        /// Gets error source.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Source { get; set; }

        /// <summary>
        /// Gets underlying exception type.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets stack trace string.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets inner error.
        /// </summary>
        [DataMember(IsRequired = false)]
        public Error InnerError { get; set; }

        public override string ToString()
        {
            return string.Format("An exception of type {0} at {1}: {2}\n{3}", ExceptionType, Source, Message, StackTrace);
        }
    }
}
