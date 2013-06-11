using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.Shared.Classes.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(ITaskProvider))]
    internal class ContractForITaskProvider : ITaskProvider
    {
        #region Implementation of ITaskProvider

        /// <summary>
        /// Gets a <see cref="TimeSpan"/> that defines worker notifications interval.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="TimeSpan"/> that contains worker notification period.
        /// </returns>
        public TimeSpan QueryNotifyInterval()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Request a task for execution.
        /// </summary>
        /// <returns>
        /// Blocks current thread until a task will be available for execution.
        /// </returns>
        public Task GetTask()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> was rejected for execution.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void RejectTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has thrown an exception <paramref name="exception"/>.  
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        /// <param name="errorReport">
        /// Error report.
        /// </param>
        public void FailTask(Guid taskId, ErrorReport errorReport)
        {
            Contract.Requires<ArgumentException>(errorReport != null);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> has been successfully executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void CompleteTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Informs scheduler that task with id = <paramref name="taskId"/> is being executed.
        /// </summary>
        /// <param name="taskId">
        /// Task's unique identifier.
        /// </param>
        public void NotifyWorker(Guid taskId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
