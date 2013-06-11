﻿using System;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.Shared.Classes;

namespace TestTasks
{
    public class Fail : ITask
    {
        #region Implementation of ITask

        /// <summary>
        /// Gets and sets an instance of <see cref="AISTek.DataFlow.Shared.Classes.IRepositoryService"/> that allows access to file repository.
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
        /// An instance of <see cref="AISTek.DataFlow.Shared.Classes.ISystemResources"/> that provides information about available system resources.
        /// </param>
        /// <returns>
        /// true if current task can be executed efficiently enough on specified resources, false otherwise.
        /// </returns>
        public bool Validate(ISystemResources resources)
        {
            return true;
        }

        /// <summary>
        /// Executes current task.
        /// </summary>
        public void Execute()
        {
            throw new ApplicationException(string.Format("Task {0} must be failed!", typeof(Fail)));
        }

        /// <summary>
        /// Teardowns the task's used resources.
        /// </summary>
        public void Teardown()
        {

        }

        #endregion
    }
}
