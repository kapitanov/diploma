using System;
using System.Collections.Generic;

namespace AISTek.DataFlow.MasterNode.DataModel
{
    /// <summary>
    /// Represents a task's entrypoint.
    /// Entrypoint is a set of:
    /// <list>
    /// <item>
    /// fully quialified entrypoint class name,
    /// </item>
    /// <item>
    /// an UID of assembly file that contains entrypoint class,
    /// </item>
    /// <item>
    /// a list of UIDs of all assembly files, which are required 
    /// to load the entrypoint class's assembly and instantiate the entrypoint class.
    /// </item> 
    /// </list>
    /// </summary>
    public class EntryPoint
    {
        /// <summary>
        /// Initializes new instance of <see cref="EntryPoint"/>
        /// </summary>
        public EntryPoint()
        {
            QualifiedClassName = string.Empty;
            DependentAssemblyIds = new List<Guid>();
        }

        /// <summary>
        /// Assembly file's unique identifier
        /// </summary>
        public Guid AssemblyId { get; set; }

        /// <summary>
        /// Fully qualified name of entry point class.
        /// </summary>
        public string QualifiedClassName { get; set; }

        /// <summary>
        /// List of additional assembly dependencies.
        /// </summary>
        public List<Guid> DependentAssemblyIds { get; set; }
    }
}
