using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.JobBuilder;

namespace AISTek.DataFlow.MasterNode.Scheduler.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(IJobBuildingPipeline))]
    internal class ContractForIJobBuildingPipeline : IJobBuildingPipeline
    {
        #region Implementation of IJobBuildingPipeline

        /// <summary>
        /// Gets a mutable list of all availible build strategies.
        /// </summary>
        public IList<IJobBuildStrategy> Strategies
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Builds the job on job start event.
        /// </summary>
        /// <param name="job">
        /// Job to build.
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
