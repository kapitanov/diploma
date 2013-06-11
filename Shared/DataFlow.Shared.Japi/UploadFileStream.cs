using System;
using System.IO;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Japi
{
    /// <summary>
    /// Represents a request to upload a file from a file stream.
    /// </summary>
    [Serializable]
    public class UploadFileStream : File
    {
        public UploadFileStream(string name, Stream fileStream)
            : base(name)
        {
            FileStream = fileStream;
        }

        protected override FileLink SaveFilePerform(IRepository repository)
        {
            using (FileStream)
            {
                var id = repository.UploadNew(Name, FileStream);
                var link = new FileLink { Id = id, Name = Name, Metadata = Metadata };
                return link;
            }
        }

        public Stream FileStream { get; private set; }
    }
}
