using System;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Provides report for task execution errors.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    public class ErrorReport
    {
        /// <summary>
        /// Initializes new instance of <see cref="ErrorReport"/> class.
        /// </summary>
        public ErrorReport()
        {
            Task = new Task();
            Source = ErrorSource.Unknown;
            Error = new Error();
        }

        /// <summary>
        /// Initializes new instance of <see cref="ErrorReport"/> class.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that caused current exception.
        /// </param>
        /// <param name="exception">
        /// An instance of <see cref="Exception"/> class that caused current error.
        /// </param>
        /// <param name="source">
        /// An <see cref="ErrorSource"/> that caused current error.
        /// </param>
        public ErrorReport(Task task, Exception exception, ErrorSource source)
        {
            Task = task;
            Error = new Error(exception);
            Source = source;
        }

        /// <summary>
        /// Gets an <see cref="ErrorSource"/> for current error.
        /// </summary>
        [DataMember(IsRequired = true)]
        public ErrorSource Source { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="Task"/> that caused current error.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Task Task { get; set; }

        /// <summary>
        /// Gets an instance of <see cref="Error"/> that describes current error.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Error Error { get; set; }
    }
}
