using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Japi;
using AISTek.DataFlow.Shared.Classes;
using System.IO;

namespace AISTek.DataFlow.Sdk.Samples.TextProcessing
{
    public class UploadFilePath : AISTek.DataFlow.Shared.Japi.File
    {
        public UploadFilePath(string path)
            : base(System.IO.Path.GetFileName(path))
        {
            Path = path;
        }

        public string Path { get; private set; }

        protected override AISTek.DataFlow.Shared.Classes.FileLink SaveFilePerform(AISTek.DataFlow.Shared.Classes.IRepository repository)
        {
            var buffer = Encoding.UTF8.GetBytes(Path);
            var id = repository.UploadNew(Name, new MemoryStream(buffer));
            var link = new FileLink { Id = id, Name = Name, Metadata = Metadata };
            return link;
        }
    }
}
