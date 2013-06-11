using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.MasterNode.Scheduler.Contracts;
using AISTek.DataFlow.Shared.Classes;
using Task = AISTek.DataFlow.MasterNode.DataModel.Task;

namespace AISTek.DataFlow.MasterNode.Scheduler
{
    /// <summary>
    /// A task's manager contract.
    /// </summary>
    [ContractClass(typeof(ContractForITaskQueue))]
    public interface ITaskQueue
    {
        #region Methods

        #region Task management

        /// <summary>
        /// Registers a task in task manager and sets its UID.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="DataModel.Task"/>.
        /// </param>
        void RegisterTask(Task task);

        /// <summary>
        /// Starts execution of specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        void StartTask(Task task);

        /// <summary>
        /// Cancels exectuion of specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        void CancelTask(Task task);

        /// <summary>
        /// Reinitalizes specified task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        void RestartTask(Task task);

        /// <summary>
        /// Removes task from task manager.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/>.
        /// </param>
        void DeleteTask(Task task);  

        #endregion

        #region Task execution queue

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
        Task PickTask(IWorkerChannel worker);

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
        Task PickTask(IWorkerChannel worker, TimeSpan timeout);

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has been executed successfully.
        /// Task queue performs recalculation of tasks' dependencies.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that has been executed.
        /// </param>
        void TaskCompleted(Task task);

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has been rejected by worker.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that has been executed.
        /// </param>
        void TaskRejected(Task task);

        /// <summary>
        /// Informs task queue that specified <paramref name="task"/> has failed.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that failed.
        /// </param>
        /// <param name="errorReport">
        /// Error report.
        /// </param>
        void TaskFailed(Task task, ErrorReport errorReport);

        #endregion

        #endregion
    }
}
