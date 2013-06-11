using System;
using System.Security;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Provides methods for runtime resource enumeration.
    /// </summary>
    public interface IRuntimeResources
    {
        /// <summary>
        /// Gets version of CLR runtime for current process.
        /// </summary>
        Version ClrVersion { get; }

        /// <summary>
        /// Gets version of DataFlow runtime for current process.
        /// </summary>
        Version RuntimeVersion { get; }

        /// <summary>
        /// Gets an instance of <see cref="PermissionSet"/> describing security permissions for current application domain.
        /// </summary>
        PermissionSet Permissions { get; }
    }
}
