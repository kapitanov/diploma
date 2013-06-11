using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.Contracts;

namespace AISTek.DataFlow.MasterNode.Scheduler.JobBuilder
{
    /// <summary>
    /// Defines interface for job building pipeline.
    /// </summary>
    [ContractClass(typeof(ContractForIJobBuildingPipeline))]
    public interface IJobBuildingPipeline
    {
        /// <summary>
        /// Gets a mutable list of all availible build strategies.
        /// </summary>
        IList<IJobBuildStrategy> Strategies { get; }

        /// <summary>
        /// Builds the job on job start event.
        /// </summary>
        /// <param name="job">
        /// Job to build.
        /// </param>
        void Build(Job job);
    }
}


