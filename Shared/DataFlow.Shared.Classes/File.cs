using System;
using System.IO;
using System.ServiceModel;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Message contract for file upload and download operations.
    /// </summary>
    [MessageContract(WrapperNamespace = Namespaces.Repository.Namespace)]
    [Serializable]
    public sealed class File
    {
        /// <summary>
        /// Initializes new instance of <see cref="File"/>.
        /// </summary>
        public File()
        { }

        /// <summary>
        /// Initializes new instance of <see cref="File"/>.
        /// </summary>
        /// <param name="link">
        /// An instance of <see cref="FileLink"/> class that contains file's reference.
        /// </param>
        public File(FileLink link)
        {
            Id = link.Id;
            Name = link.Name;
        }

        /// <summary>
        /// Gets and sets file's UID.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets and sets file's name
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets and sets file's stream.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream FileStream { get; set;}
    }
}
