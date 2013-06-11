using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// File link DTO's data contract
    /// </summary>
    [DataContract(Namespace = Namespaces.Scheduler.Namespace)]
    [Serializable]
    public class FileLink
    {
        /// <summary>
        /// Initializes new instance of <see cref="FileLink"/>
        /// </summary>
        public FileLink()
        {
            Metadata = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates an instance of <see cref="FileLink"/> that points to whole file's content.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of file.
        /// </param>
        /// <returns>
        /// An instance of <see cref="FileLink"/> that points to whole file's content.
        /// </returns>
        public static FileLink File(Guid id)
        {
            return new FileLink { Id = id };
        }

        /// <summary>
        /// File's unique identifier.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// File's non-unique name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// File's metadata.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Dictionary<string, string> Metadata { get; set; }
    }
}


