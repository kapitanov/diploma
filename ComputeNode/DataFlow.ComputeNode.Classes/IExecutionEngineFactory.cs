using System.Configuration;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.ComputeNode.Classes.Contracts;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Defines methods to create of <see cref="IExecutionEngine"/> impementation classes. 
    /// </summary>
    [ContractClass(typeof(ContractForIExecutionEngineFactory))]
    public interface IExecutionEngineFactory
    {
        /// <summary>
        /// Configures the factory.
        /// </summary>
        /// <param name="configuration">
        /// An instance of <see cref="System.Configuration.Configuration"/> that is mapped to current runtime.
        /// </param>
        void Setup(Configuration configuration);

        /// <summary>
        /// Creates an instance of execution engine.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IExecutionEngine"/> implementation.
        /// </returns>
        IExecutionEngine CreateEngine();
    }
}
