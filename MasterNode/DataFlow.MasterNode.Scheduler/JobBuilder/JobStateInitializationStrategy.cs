using AISTek.DataFlow.MasterNode.DataModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.MasterNode.Scheduler.JobBuilder
{
    public class JobStateInitializationStrategy : IJobBuildStrategy
    {
        #region Implementation of IJobBuildStrategy

        /// <summary>
        /// Perfoms build state on job start event.
        /// </summary>
        /// <param name="job">
        /// Job to start.
        /// </param>
        public void Build(Job job)
        {
            Logger.Write(LogCategories.Information(string.Format("Initializing job {{{0}}}", job), LogCategories.TaskServices));
            job.InitJobState();
        }

        #endregion
    }
}
