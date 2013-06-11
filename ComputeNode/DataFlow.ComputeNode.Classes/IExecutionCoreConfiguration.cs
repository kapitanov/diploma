using System;

namespace AISTek.DataFlow.ComputeNode.Classes
{
    /// <summary>
    /// Provides confgiruration information for <see cref="IExecutionCore"/> implementations.
    /// </summary>
    public interface IExecutionCoreConfiguration
    {
        /// <summary>
        /// Gets a type of <see cref="IExecutionEngine"/> implementation that must be run inside the sandbox.
        /// </summary>
        Type Sandbox { get; }
    }
}
