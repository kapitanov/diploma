using System;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.ComputeNode.Classes.Contracts
{
    [ContractClassFor(typeof(IExecutionCore))]
    internal class ContractForIExecutionCore : IExecutionCore
    {
        #region Implementation of IExecutionCore

        /// <summary>
        /// Configures the execution core.
        /// </summary>
        /// <param name="configuration">
        /// An instance of <see cref="IExecutionCoreConfiguration"/> that is provides configuration information.
        /// </param>
        public void Setup(IExecutionCoreConfiguration configuration)
        {
            Contract.Requires(configuration != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts the task execution workflow and returns after its final.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ExecutionResult"/> that contains task execution workflow result.
        /// </returns>
        public ExecutionResult StartExecution()
        {
            Contract.Ensures(Contract.Result<ExecutionResult>() != null);

            throw new NotImplementedException();
        }

        #endregion
    }
}
