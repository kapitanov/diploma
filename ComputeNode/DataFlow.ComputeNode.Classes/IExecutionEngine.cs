using System.Diagnostics.Contracts;
using AISTek.DataFlow.ComputeNode.Classes.Contracts;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Defines the methods to execute the task.
    /// </summary>
    [ContractClass(typeof(ContractForIExecutionEngine))]
    public interface IExecutionEngine
    {
        /// <summary>
        /// Gets and sets a value of <see cref="ISystemResources"/> that provides information of availible system resources.
        /// </summary>
        ISystemResources SystemResources { get; set; }

        /// <summary>
        /// Gets and sets an instance of <see cref="IRepositoryService"/> that allows access to file repository.
        /// </summary>
        IRepository Repository { get; set; }

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
        void ExecuteTask(Task task);
    }
}
