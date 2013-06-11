using System;
using System.Linq;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Threading;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class ValidateGraph : Stage
    {
        public ValidateGraph()
            : base("Validating job")
        { }

        #region Overrides of Stage

        public override void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            updateStatus("Validating job's graph...");
            var index = 0;
            var count = job.Tasks.Count;
            if(!job.Tasks.All(task => RecoursiveCheckGraph(task, ImmutableList<Japi.Task>.Empty)))
            {
                throw new Exception("Job is invalid: graph contains cycles.");
            }
        }

        #endregion

        private static bool RecoursiveCheckGraph(Japi.Task root, ImmutableList<Japi.Task> previousTasks)
        {
            if(previousTasks.Contains(root))
            {
                Console.WriteLine("1: {0}", root);
                return false;
            }

            foreach(var task in root.Dependencies)
            {
                if(previousTasks.Contains(task))
                {
                    Console.WriteLine("2: {0}", root);
                    return false;
                }

                if(!RecoursiveCheckGraph(task, previousTasks.Add(root)))
                    return false;
            }
            return true;
        }
    }
}
