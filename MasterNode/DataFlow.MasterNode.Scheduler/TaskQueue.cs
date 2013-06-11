using System;
using System.Collections.Generic;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Threading;
using System.Diagnostics.Contracts;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using JobState = AISTek.DataFlow.MasterNode.DataModel.JobState;
using Task = AISTek.DataFlow.MasterNode.DataModel.Task;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// Task queue class.
    /// </summary>
    /// TODO: Perform double locks on ITaskQueue methods
    public class TaskQueue : ITaskQueue
    {
        #region Implementation of ITaskQueue

        #region Methods

        /// <summary>
        /// Registers a task in task manager and sets its UID.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="DataModel.Task"/>.
        /// </param>
        public void RegisterTask(Task task)
        {
            lock (task)
            {
                ToUnavailible(task);
            }
        }

        /// <summary>
        /// Starts execution of specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void StartTask(Task task)
        {
            lock (task)
            {
                ResetDependencyCounter(task);
                FromUnavailible(task);
                if (task.DependenciesCount == 0)
                {
                    ToPending(task);
                }
                else
                {
                    ToNotReady(task);
                }
            }
        }

        /// <summary>
        /// Cancels execution of specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void CancelTask(Task task)
        {
            lock (task)
            {
                switch (task.State)
                {
                    case TaskState.Processing:
                        FromProcessing(task);
                        break;
                    case TaskState.Pending:
                        FromPending(task);
                        break;
                    case TaskState.NotReady:
                        FromNotReady(task);
                        break;
                    default:
                        throw new InvalidOperationException(
                            string.Format("Task {0} has unexpected state {1} on TaskQueue::CancelTask()", task, task.State));
                }

                ToCancelled(task);
            }
        }

        /// <summary>
        /// Reinitializes specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void RestartTask(Task task)
        {
            lock (task)
            {
                FromCancelled(task);
                ToUnavailible(task);
                StartTask(task);
                ResetDependencyCounter(task);
            }
        }

        /// <summary>
        /// Removes task from task manager.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void DeleteTask(Task task)
        {
            lock (task)
            {
                switch (task.State)
                {
                    case TaskState.NotReady:
                        FromNotReady(task);
                        break;

                    case TaskState.Completed:
                        FromCompleted(task);
                        break;

                    case TaskState.Failed:
                        FromFailed(task);
                        break;

                    case TaskState.Cancelled:
                        FromCancelled(task);
                        break;

                    default:
                        throw new InvalidOperationException(
                           string.Format("Task {0} has unexpected state {1} on TaskQueue::DeleteTask()", task, task.State));
                }

                task.State = TaskState.Deleted;
            }
        }

        #endregion

        #endregion

        #region Implementation of ITaskQueue

        /// <summary>
        /// Pick a task for execution and sets specified <paramref name="worker"/> for it.
        /// </summary>
        /// <param name="worker">
        /// An instance of <see cref="IWorkerChannel"/> that specifies  communication channel for task's executor
        /// </param>
        /// <returns>
        /// An instance of <see cref="Task"/> that is ready for execution.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method blocks calling thread until a task is picked.
        /// </para>
        /// <para>
        /// After picking a task, task queue ensures that the picked task will not be picked anymore.
        /// </para> 
        /// </remarks>
        public Task PickTask(IWorkerChannel worker)
        {
            Task task;
            lock (task = pendingTasks.Dequeue())
            {
                task.State = TaskState.Processing;
                task.Worker = worker;
                return task;
            }
        }

        /// <summary>
        /// Pick a task for execution and sets specified <paramref name="worker"/> for it.
        /// </summary>
        /// <param name="worker">
        /// An instance of <see cref="IWorkerChannel"/> that specifies  communication channel for task's executor
        /// </param>
        /// <param name="timeout">
        /// Operation timeout.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Task"/> that is ready for execution.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This method blocks calling thread until a task is picked.
        /// </para>
        /// <para>
        /// After picking a task, task queue ensures that the picked task will not be picked anymore.
        /// </para> 
        /// </remarks>
        public Task PickTask(IWorkerChannel worker, TimeSpan timeout)
        {
            Task task;
            try
            {
            lock (task = pendingTasks.Dequeue(timeout))
            {
                task.State = TaskState.Processing;
                task.Worker = worker;
                return task;
            }
            }
            catch (TimeoutException)
            {
                return null;
            }
        }

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has been executed successfully.
        /// Task queue performs recalculation of tasks' dependencies.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that has been executed.
        /// </param>
        public void TaskCompleted(Task task)
        {
            lock (task)
            {
                if (task.State == TaskState.Failed)
                {
                    // Job failed, there is nothing else to do. The task is already in 'failed' list.
                    return;
                }

                FromProcessing(task);
                ToCompleted(task);

                // Recalculate dependents' states
                foreach (var t in task.Outputs)
                {
                    lock (t)
                    {
                        if (t.State == TaskState.NotReady)
                        {
                            t.DependenciesCount--;
                            if (t.DependenciesCount == 0)
                            {
                                t.State = TaskState.Pending;
                                FromNotReady(t);
                                ToPending(t);
                            }
                        }
                    }
                }

                task.Job.NotifyTaskCompleted(task);
            }
        }

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has been rejected by worker.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that has been executed.
        /// </param>
        public void TaskRejected(Task task)
        {
            lock (task)
            {
                FromProcessing(task);
                ToPending(task);
            }
        }

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has failed.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that failed.
        /// </param>
        /// <param name="errorReport">
        /// Error report.
        /// </param>
        public void TaskFailed(Task task, ErrorReport errorReport)
        {
            lock (task)
            {
                FromProcessing(task, terminateProcessing: false);
                ToFailed(task);
                task.Error = errorReport;

                var job = task.Job;
                lock (job)
                {
                    Logger.Write(
                        LogCategories.Error(
                            string.Format("Task {0} failed because of {1}. This caused job {2} to fail.", task,
                                          errorReport.Error, task.Job),
                            LogCategories.TaskServices,
                            LogCategories.TaskQueue));

                    job.State = JobState.Failed;

                    foreach (var t in task.Job.Tasks)
                    {
                        lock (t)
                        {
                            switch (t.State)
                            {
                                case TaskState.NotReady:
                                    FromNotReady(t);
                                    break;

                                case TaskState.Processing:
                                    FromProcessing(t);
                                    break;

                                case TaskState.Failed:
                                    continue;

                                case TaskState.Pending:
                                    FromPending(t);
                                    break;

                                case TaskState.Completed:
                                    FromCompleted(t);
                                    break;

                                default:
                                    throw new InvalidOperationException(
                                        string.Format("Task {0} has unexpected state {1} on TaskQueue::TaskFailed()", task, task.State));
                            }

                            ToFailed(t);
                        }
                    }
                }
            }
        }

        #endregion

        #region Private fields

        private readonly IList<Task> unavailibleTasks = new List<Task>();

        private readonly IList<Task> notReadyTasks = new List<Task>();

        private readonly BlockingQueue<Task> pendingTasks = new BlockingQueue<Task>();

        private readonly IList<Task> processingTasks = new List<Task>();

        private readonly IList<Task> completedTasks = new List<Task>();

        private readonly IList<Task> failedTasks = new List<Task>();

        private readonly IList<Task> cancelledTasks = new List<Task>();

        #endregion

        #region Private methods

        private void ToUnavailible(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State != TaskState.Unavailible);
            Contract.Ensures(task.State == TaskState.Unavailible);

            task.State = TaskState.Unavailible;
            lock (unavailibleTasks)
            {
                unavailibleTasks.Add(task);
            }
        }

        private void ToNotReady(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State != TaskState.NotReady);
            Contract.Ensures(task.State == TaskState.NotReady);

            task.State = TaskState.NotReady;
            lock (notReadyTasks)
            {
                notReadyTasks.Add(task);
            }
        }

        private void ToPending(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State != TaskState.Pending);
            Contract.Ensures(task.State == TaskState.Pending);

            task.State = TaskState.Pending;
            pendingTasks.Enqueue(task);
        }

        private void ToCancelled(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State != TaskState.Cancelled);
            Contract.Ensures(task.State == TaskState.Cancelled);

            task.State = TaskState.Cancelled;
            lock (cancelledTasks)
            {
                cancelledTasks.Add(task);
            }
        }

        private void ToCompleted(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State != TaskState.Completed);
            Contract.Ensures(task.State == TaskState.Completed);

            task.State = TaskState.Completed;
            lock (completedTasks)
            {
                completedTasks.Add(task);
            }
        }

        private void ToFailed(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State != TaskState.Failed);
            Contract.Ensures(task.State == TaskState.Failed);

            task.State = TaskState.Failed;
            lock (failedTasks)
            {
                failedTasks.Add(task);
            }
        }

        private void FromUnavailible(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            lock (unavailibleTasks)
            {
                unavailibleTasks.Remove(task);
            }
        }

        private void FromNotReady(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            lock (notReadyTasks)
            {
                notReadyTasks.Remove(task);
            }
        }

        private void FromPending(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            pendingTasks.Remove(task);
        }

        private void FromCancelled(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            lock (cancelledTasks)
            {
                cancelledTasks.Remove(task);
            }
        }

        private void FromFailed(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            lock (failedTasks)
            {
                failedTasks.Remove(task);
            }
        }

        private void FromCompleted(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            lock (completedTasks)
            {
                completedTasks.Remove(task);
            }
        }

        private void FromProcessing(Task task, bool terminateProcessing = true)
        {
            Contract.Requires<ArgumentNullException>(task != null);

            if (terminateProcessing)
            {
                var worker = task.Worker as IWorkerChannel;
                if (worker != null)
                {
                    worker.Cancel();
                }
            }

            lock (processingTasks)
            {
                processingTasks.Remove(task);
            }
        }


        private static void ResetDependencyCounter(Task task)
        {
            task.DependenciesCount = (uint)task.Inputs.Count;
        }

        #endregion
    }
}
