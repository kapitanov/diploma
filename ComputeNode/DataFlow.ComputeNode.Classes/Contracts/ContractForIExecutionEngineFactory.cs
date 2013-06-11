using System;
using System.Configuration;
using System.Diagnostics.Contracts;

namespace AISTek.DataFlow.ComputeNode.Classes.Contracts
{
    [ContractClassFor(typeof(IExecutionEngineFactory))]
    internal class ContractForIExecutionEngineFactory : IExecutionEngineFactory
    {
        #region Implementation of IExecutionEngineFactory

        /// <summary>
        /// Configures the factory.
        /// </summary>
        /// <param name="configuration">
        /// An instance of <see cref="System.Configuration.Configuration"/> that is mapped to current runtime.
        /// </param>
        public void Setup(Configuration configuration)
        {
            Contract.Requires(configuration != null);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an instance of execution engine.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IExecutionEngine"/> implementation.
        /// </returns>
        public IExecutionEngine CreateEngine()
        {
            Contract.Ensures(Contract.Result<IExecutionEngine>() != null);

            throw new NotImplementedException();
        }

        #endregion
    }
}
