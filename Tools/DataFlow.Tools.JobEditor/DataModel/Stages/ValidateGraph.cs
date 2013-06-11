using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Threading;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class ValidateGraph : Stage
    {
        public ValidateGraph()
            : base("Validating job...")
        { }

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job,
            Action<int, int> notifyProgress,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            updateStatus("Validating job's graph...");
            var index = 0;
            var count = job.Tasks.Count;
            if (!job.Tasks.All(task =>
            {
                notifyProgress(++index, count);
                return RecoursiveCheckGraph(task, ImmutableList<Guid>.Empty);
            }))
                throw new Exception("Job is invalid: graph contains cycles.");
        }

        #endregion

        private static bool RecoursiveCheckGraph(TaskRequest root, ImmutableList<Guid> previousTasks)
        {
            if (previousTasks.Contains(root.Id))

                return false;

            foreach (var task in root.Dependencies)
            {
                if (previousTasks.Contains(task.Id))
                    return false;

                if (!RecoursiveCheckGraph(task, previousTasks.Add(root.Id)))
                    return false;
            }
            return true;
        }
    }
}
