using System;
using System.IO;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Japi
{
    /// <summary>
    /// Represents a request to upload a file.
    /// </summary>
    [Serializable]
    public class UploadFile : File
    {
        /// <summary>
        /// Initializes new instance of <see cref="UploadFile"/>.
        /// </summary>
        /// <param name="path">
        /// Physical path to file.
        /// </param>
        /// <remarks>
        /// <see cref="File.Name"/> property is filled automatically according to physical file's name.
        /// </remarks>
        public UploadFile(string path)
            : base(System.IO.Path.GetFileName(path))
        {
            Path = path;
        }

        /// <summary>
        /// Gets a physical path to file.
        /// </summary>
        public string Path { get; private set; }

        protected override FileLink SaveFilePerform(IRepository repository)
        {
            using (var stream = new FileStream(Path, FileMode.Open))
            {
                var id = repository.UploadNew(Name, stream);
                var link = new FileLink { Id = id, Name = Name, Metadata = Metadata };
                return link;
            }
        }
    }
}
