namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Provides methods for CPU resources enumeration.
    /// </summary>
    public interface ICpuResources
    {
        /// <summary>
        /// Gets count of processor cores availible for runtime.
        /// </summary>
        uint CoresCount { get; }

        /// <summary>
        /// Gets a value indicating whether current runtime is 32bit X86.
        /// </summary>
        bool IsX86 { get; }

        /// <summary>
        /// Gets a value indicating whether current runtime is 64bit X64.
        /// </summary>
        bool IsX64 { get; }
    }
}

