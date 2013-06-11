using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.Contracts;

namespace AISTek.DataFlow.MasterNode.Scheduler.JobBuilder
{
    /// <summary>
    /// Defines interface for job building stage.
    /// </summary>
    [ContractClass(typeof(ContractForIJobBuildStrategy))]
    public interface IJobBuildStrategy
    {
        /// <summary>
        /// Perfoms build state on job start event.
        /// </summary>
        /// <param name="job">
        /// Job to start.
        /// </param>
        void Build(Job job);
    }
}