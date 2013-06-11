using System;
using System.Collections.Generic;
using System.Linq;
using Dto = AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Japi
{ 
    [Serializable]
    public sealed class EntryPoint
    {
        public EntryPoint()
        {
            References = new List<File>();
            Assembly = new UploadFile("");
        }

        public File Assembly { get; set; }

        public string ClassName { get; set; }

        public List<File> References { get; set; }

        public override string ToString()
        {
            return ClassName ?? "";
        }

        public Dto.EntryPoint ToContract()
        {
            return new Dto.EntryPoint
                       {
                           AssemblyId = Assembly.Link.Id,
                           QualifiedClassName = ClassName,
                           DependentAssemblyIds = References.Select(rf => rf.Link.Id).ToList()
                       };
        }
    }
}
