using System.ServiceModel;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Defines methods for callback notifications.
    /// </summary>
    public interface IJobManagerCallback
    {
        /// <summary>
        /// Notifies client that specified job has been completed.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/> that has been completed.
        /// </param>
        [OperationContractAttribute(
            IsOneWay = true, 
            Action = Namespaces.Scheduler.JobManager.Callback.JobCompleted)]
        void JobCompleted(Job job);

        /// <summary>
        /// Notifies client that specified job has been failed.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Job"/> that has been failed.
        /// </param>
        [OperationContractAttribute(
            IsOneWay = true,
            Action = Namespaces.Scheduler.JobManager.Callback.JobFailed)]
        void JobFailed(Job job);
    }
}


