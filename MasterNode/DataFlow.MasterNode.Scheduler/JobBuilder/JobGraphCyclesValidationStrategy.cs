using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.Shared.Classes.Exceptions;
using AISTek.DataFlow.Threading;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.MasterNode.Scheduler.JobBuilder
{
    public class JobGraphCyclesValidationStrategy : IJobBuildStrategy
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
            if (!job.Tasks.All(task => RecoursiveCheckGraph(task, ImmutableList<Guid>.Empty)))
                throw new InvalidJobException(
                    "Job graph contains cycles. ",
                    job.Id);
        }

        #endregion

        private static bool RecoursiveCheckGraph(Task root, ImmutableList<Guid> previousTasks)
        {
            Contract.Requires(root != null);
            Contract.Requires(root.Inputs != null);
            Contract.Requires(previousTasks != null);

            if (previousTasks.Contains(root.Id))
            {
                Logger.Write(
                    LogCategories.Error(string.Format(
                    "{0} is cycled.", root), 
                    LogCategories.TaskServices));
                return false;
            }

            foreach (var task in root.Inputs)
            {
                if (previousTasks.Contains(task.Id))
                {
                    Logger.Write(
                        LogCategories.Error(string.Format(
                        "{0} is cycled.", task),
                        LogCategories.TaskServices));
                    return false;
                }

                if (!RecoursiveCheckGraph(task, previousTasks.Add(root.Id)))
                    return false;
            }
            return true;
        }
    }
}
