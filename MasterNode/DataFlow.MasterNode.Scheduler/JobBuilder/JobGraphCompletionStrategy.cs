using System.Collections.Generic;
using System.Text;
using AISTek.DataFlow.MasterNode.DataModel;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AISTek.DataFlow.MasterNode.Scheduler.JobBuilder
{
    public class JobGraphCompletionStrategy : IJobBuildStrategy
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
            Logger.Write(
                    LogCategories.Information("Clearing outputs...",
                                              LogCategories.TaskServices,
                                              LogCategories.JobGraphCompletionStrategy));

            foreach (var task in job.Tasks)
            {
                task.Outputs.Clear();
            }

            Logger.Write(
                    LogCategories.Information("Building outputs...",
                                              LogCategories.TaskServices,
                                              LogCategories.JobGraphCompletionStrategy));

            foreach (var task in job.Tasks)
            {
                foreach (var input in task.Inputs)
                {
                    input.Outputs.Add(task);   
                }
            }

            Logger.Write(
                LogCategories.Information(BuildObjectGraph(job),
                LogCategories.TaskServices,
                LogCategories.JobGraphCompletionStrategy));
        }

        #endregion

        private static string BuildObjectGraph(Job job)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Task graph for job {0}:", job).AppendLine();

            foreach (var task in job.Tasks)
            {
                sb.AppendFormat("{0}:", task).AppendLine().AppendLine("Dependencies:");
                
                foreach (var input in task.Inputs)
                {
                    sb.AppendFormat(" - {0}", input).AppendLine();
                }

                sb.AppendFormat("{0}:", task).AppendLine().AppendLine("Dependents:");

                foreach (var output in task.Outputs)
                {
                    sb.AppendFormat(" - {0}", output).AppendLine();
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
