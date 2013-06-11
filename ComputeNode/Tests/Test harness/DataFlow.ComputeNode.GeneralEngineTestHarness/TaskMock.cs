using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.GeneralEngineTestHarness
{
    [Serializable]
    public class TaskMock : ITask
    {
        #region Implementation of ITask

        /// <summary>
        /// Gets and sets an instance of <see cref="IRepositoryService"/> that allows access to file repository.
        /// </summary>
        public IRepository Repository { get; set; }

        /// <summary>
        /// Gets and sets and instance of <see cref="ITask.Task"/> that contains definition of current task.
        /// </summary>
        public Task Task { get; set; }

        /// <summary>
        /// Checks system resources to comply task's requirements. 
        /// </summary>
        /// <param name="resources">
        /// An instance of <see cref="ISystemResources"/> that provides information about availible system resources.
        /// </param>
        /// <returns>
        /// true if current task can be executed efficienly enough on specified resources, false otherwise.
        /// </returns>
        public bool Validate(ISystemResources resources)
        {
            Console.WriteLine("Task is being validated.");
            return true;
        }

        /// <summary>
        /// Executes current task.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("Task is being executed.");
        }

        /// <summary>
        /// Teardowns the task's used resources.
        /// </summary>
        public void Teardown()
        {
            Console.WriteLine("Task is being teared dow.n");
        }

        #endregion
    }
}
