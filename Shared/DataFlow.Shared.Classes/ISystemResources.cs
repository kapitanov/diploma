namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Provides methods for system resource enumeration.
    /// </summary>
    public interface ISystemResources
    {
        /// <summary>
        /// Gets an instance of <see cref="ICpuResources"/> implementation that enumerates CPU resources.
        /// </summary>
        ICpuResources Cpu { get; }

        /// <summary>
        /// Gets an instance of <see cref="IMemoryResources"/> implementation that enumerates RAM resources.
        /// </summary>
        IMemoryResources Memory { get; }

        /// <summary>
        /// Gets an instance of <see cref="IRuntimeResources"/> implementation that enumerates runtime resources.
        /// </summary>
        IRuntimeResources Runtime { get; }
    }
}
