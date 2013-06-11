using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class UploadTasksExecutionStage : ExecutionStage
    {
        public UploadTasksExecutionStage(JobRuntime runtime)
            : base(runtime)
        { }

        public override string Name { get { return "UploadTasks"; } }

        public override void Execute()
        {
            var tasks = Job.Tasks.Select(TaskWithCounter.Create).ToList();

            while (tasks.Count > 0)
            {
                var task = tasks.First(TaskWithCounter.HasNoDependencies).Task;

                var updatedTasks = from t in tasks
                                   where t.Task != task
                                   select TaskWithCounter.Update(t, task);

                tasks = updatedTasks.ToList();
                task.Id = Runtime.Connection.JobManager.CreateTask(task.ToContract());
            }
        }

        private class TaskWithCounter
        {
            public TaskWithCounter(Japi.Task task, int dependencies)
            {
                Task = task;
                Dependencies = dependencies;
            }

            public Japi.Task Task { get; private set; }

            public int Dependencies { get; private set; }

            public static TaskWithCounter Create(Japi.Task task)
            {
                return new TaskWithCounter(task, task.Dependencies.Count);
            }

            public static bool HasNoDependencies(TaskWithCounter item)
            {
                return item.Dependencies <= 0;
            }

            public static TaskWithCounter Update(TaskWithCounter currentTask, Japi.Task selectedTask)
            {
                if (currentTask.Task.Dependencies.Contains(selectedTask))
                {
                    return new TaskWithCounter(currentTask.Task, currentTask.Dependencies - 1);
                }

                return new TaskWithCounter(currentTask.Task, currentTask.Dependencies);
            }
        }
    }
}
