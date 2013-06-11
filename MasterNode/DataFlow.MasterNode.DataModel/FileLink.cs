using System;
using System.Collections.Generic;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.MasterNode.DataModel
{
    /// <summary>
    /// Represents a link to file.
    /// </summary>
    public class FileLink
    {
        public FileLink(Guid id)
            : this(id, string.Empty, null)
        { }

        public FileLink(Guid id, string name, IDictionary<string ,string> metadata)
        {
            Id = id;
            Name = name;

            if(metadata != null)
                Metadata = new Dictionary<string, string>(metadata);
            else
                Metadata = new Dictionary<string, string>();
        }

        /// <summary>
        /// File's unique identifier
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// File's non-unique name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// File's metadata.
        /// </summary>
        public IDictionary<string, string> Metadata { get; private set; }
    }
}
