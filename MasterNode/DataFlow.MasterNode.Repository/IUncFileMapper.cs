using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.MasterNode.Repository
{
    public interface IUncFileMapper
    {
        UncFileMapping MapFile(Guid fileId, string fileName);
    }
}
