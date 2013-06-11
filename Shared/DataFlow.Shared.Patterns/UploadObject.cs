using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.DataExchange;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal class UploadObject<T> : Japi.File
    {
        public UploadObject(string filename, T obj, DataContractOptions dtoOptions = null)
            : base(filename)
        {
            Object = obj;
            this.dtoOptions = dtoOptions ?? DataContractOptions.Defaut;
        }
        
        public T Object { get; private set; }

        protected override Classes.FileLink SaveFilePerform(Classes.IRepository repository)
        {
            var id = Object.Serialize(dtoOptions).ToNewFile(repository, Name);
            var link = new FileLink { Id = id, Name = Name, Metadata = Metadata };
            return link;
        }

        private readonly DataContractOptions dtoOptions;
    }
}
