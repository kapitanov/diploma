using System.Diagnostics.Contracts;
using AISTek.DataFlow.ComputeNode.Classes.Contracts;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Defines the methods for execution core that defines the task execution workflow.
    /// </summary>
    [ContractClass(typeof(ContractForIExecutionCore))]
    public interface IExecutionCore
    {
        /// <summary>
        /// Configures the execution core.
        /// </summary>
        /// <param name="configuration">
        /// An instance of <see cref="IExecutionCoreConfiguration"/> that is provides configuration information.
        /// </param>
        void Setup(IExecutionCoreConfiguration configuration);

        /// <summary>
        /// Starts the task execution workflow and returns after its final.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ExecutionResult"/> that contains task execution workflow result.
        /// </returns>
        ExecutionResult StartExecution();
    }
}
