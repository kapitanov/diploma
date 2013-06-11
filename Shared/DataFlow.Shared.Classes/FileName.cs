using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Classes
{
    public sealed class FileName
    {
        public FileName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        
        public string Name { get; private set; }
    }
}
