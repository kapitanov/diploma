using System;

namespace AISTek.DataFlow.MasterNode.DataModel
{
    /// <summary>
    /// Represent a file's info.
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// File's unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// File's nonunique name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File's data length.
        /// </summary>
        public ulong Length { get; set; }
    }
}
