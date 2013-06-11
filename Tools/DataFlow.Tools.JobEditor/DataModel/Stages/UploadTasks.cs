using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class UploadTasks : Stage
    {
        public UploadTasks()
            : base("Uploading tasks...")
        { }

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job,
            Action<int, int> notifyProgress,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            var tasks = (from t in job.Tasks
                        select new {Task = t, Dependencies = t.Dependencies.Count}).ToList();

            while (tasks.Count() > 0)
            {
                var task = (from t in tasks
                           where t.Dependencies == 0
                           select t.Task).First();

                updateStatus(string.Format("Creating task \"{0}\"...", task.Name));

                tasks = (from t in tasks
                        where t.Task != task
                        let dependencyList = t.Task.Dependencies
                        let dependencyCount = t.Dependencies
                        select new
                                   {
                                       Task = t.Task,
                                       Dependencies = dependencyList.Contains(task)
                                                          ?
                                                             dependencyCount - 1
                                                          :
                                                             dependencyCount
                                   }).ToList();

                task.Id = jobManager.CreateTask(task.ToContract());
                notifyProgress(tasks.Count(), job.Tasks.Count);
            }
        }

        #endregion
    }
}
