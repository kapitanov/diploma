using System;
using System.Collections.Generic;
using System.Linq;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    [Serializable]
    public class EntryPointDefinition
    {
        public EntryPointDefinition()
        {
            References = new List<FileRequest>();
            Assembly = new UploadFileRequest("");
        }

        public FileRequest Assembly { get; set; }

        public string ClassName { get; set; }

        public List<FileRequest> References { get; set; }

        public override string ToString()
        {
            return ClassName ?? "";
        }

        public EntryPoint ToContract()
        {
            return new EntryPoint
                       {
                           AssemblyId = Assembly.Link.Id,
                           QualifiedClassName = ClassName,
                           DependentAssemblyIds = References.Select(rf => rf.Link.Id).ToList()
                       };
        }
    }
}
