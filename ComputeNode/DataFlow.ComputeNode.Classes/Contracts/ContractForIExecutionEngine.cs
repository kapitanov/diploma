using System;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Classes.Contracts
{
    [ContractClassFor(typeof(IExecutionEngine))]
    internal class ContractForIExecutionEngine : IExecutionEngine
    {
        #region Implementation of IExecutionEngine

        /// <summary>
        /// Gets and sets a value of <see cref="ISystemResources"/> that provides information of availible system resources.
        /// </summary>
        public ISystemResources SystemResources
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets and sets an instance of <see cref="IRepositoryService"/> that allows access to file repository.
        /// </summary>
        public IRepository Repository
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="task">
        /// An instance of <see cref="Task"/> that must be executed.
        /// </param>
        /// <exception cref="TaskRejectedException">
        /// Occurs if there's not enogh system resources to execute specified task efficiently.
        /// </exception> 
        /// <exception cref="TaskFailedException">
        /// Occurs when execution engine fails to execute the specified task.
        /// </exception> 
        public void ExecuteTask(Task task)
        {
            Contract.Requires(task != null);

            throw new NotImplementedException();
        }

        #endregion
    }
}
