using System;
using System.IO;
using AISTek.DataFlow.Shared.Classes;
using File = AISTek.DataFlow.Shared.Classes.File;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    [Serializable]
    public class UploadFileRequest : FileRequest
    {
        public UploadFileRequest(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileName(path);
        }

        public string Path { get; private set; }

        #region Overrides of FileRequest

        public override FileLink SaveFile(IRepository repository)
        {
            using (var stream = new FileStream(Path, FileMode.Open))
            {
                var id = repository.UploadNew(Name, stream);
                var link = new FileLink { Id = id, Name = Name };
                Link = link;
                return link;
            }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
