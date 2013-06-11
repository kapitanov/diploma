using System;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    [Serializable]
    public class CreateFileRequest : FileRequest
    {
        public CreateFileRequest (string name)
        {
            Name = name;
        }

        #region Overrides of FileRequest

        public override FileLink SaveFile(IRepository repository)
        {
            var id = repository.CreateNew(Name);
            var link = new FileLink { Id = id };
            Link = link;
            return link;
        }

        #endregion
        
        public override string ToString()
        {
            return Name;
        }
    }
}
