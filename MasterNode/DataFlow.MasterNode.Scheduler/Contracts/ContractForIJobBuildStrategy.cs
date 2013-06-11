using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.JobBuilder;

namespace AISTek.DataFlow.MasterNode.Scheduler.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(IJobBuildStrategy))]
    internal class ContractForIJobBuildStrategy : IJobBuildStrategy
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
            Contract.Requires(job != null);
            Contract.Requires(job.State == JobState.Ready);

            throw new NotImplementedException();
        }

        #endregion
    }
}


