namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Provides methods for RAM resource enumeration.
    /// </summary>
    public interface IMemoryResources
    {
        /// <summary>
        /// Gets an amount of total memory present in system (in Mb).
        /// </summary>
        uint TotalMemory { get; }

        /// <summary>
        /// Gets an amount of free memory in system availible for runtime (in Mb).
        /// </summary>
        uint FreeMemory { get; }
    }
}
