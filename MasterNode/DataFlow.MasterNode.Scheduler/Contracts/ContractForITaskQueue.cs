using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.Shared.Classes;
using Task = AISTek.DataFlow.MasterNode.DataModel.Task;

namespace AISTek.DataFlow.MasterNode.Scheduler.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(ITaskQueue))]
    internal class ContractForITaskQueue : ITaskQueue
    {
        #region Implementation of ITaskManager

        #region Task management method contacts

        /// <summary>
        /// Registers a task in task manager and sets its UID.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="DataModel.Task"/>.
        /// </param>
        public void RegisterTask(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State == TaskState.Unattached);
            Contract.Ensures(task.State == TaskState.Unavailible);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts execution of specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void StartTask(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State == TaskState.Unavailible);
            Contract.Ensures(task.State == TaskState.Pending ||
                             task.State == TaskState.NotReady);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancels execution of specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void CancelTask(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(
                task.State == TaskState.NotReady ||
                task.State == TaskState.Pending ||
                task.State == TaskState.Processing);
            Contract.Ensures(task.State == TaskState.Cancelled);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Reinitializes specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void RestartTask(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State == TaskState.Cancelled);
            Contract.Ensures(task.State == TaskState.Unavailible);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes task from task manager.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        public void DeleteTask(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(
                task.State == TaskState.Completed ||
                task.State == TaskState.Failed ||
                task.State == TaskState.Cancelled ||
                task.State == TaskState.Unavailible);
            Contract.Ensures(task.State == TaskState.Deleted);

            throw new NotImplementedException();
        }

        #endregion

        #region Task execution method contracts

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
            Contract.Requires(worker != null);

            throw new NotImplementedException();
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
            Contract.Requires(worker != null);

            throw new NotImplementedException();
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
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State == TaskState.Processing);
            Contract.Ensures(task.State == TaskState.Completed);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has been rejected by worker.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that has been executed.
        /// </param>
        public void TaskRejected(Task task)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentException>(task.State == TaskState.Processing);
            Contract.Ensures(task.State == TaskState.Pending);

            throw new NotImplementedException();
        }


        void ITaskQueue.TaskFailed(Task task, ErrorReport errorReport)
        {
            Contract.Requires<ArgumentNullException>(task != null);
            Contract.Requires<ArgumentNullException>(errorReport != null);
            Contract.Requires<ArgumentException>(task.State == TaskState.Processing);
            Contract.Ensures(task.State == TaskState.Failed);
            Contract.Ensures(task.Error == errorReport);

            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}


