using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.MasterNode.Repository
{
    public class UncFileMapperStub : IUncFileMapper
    {
        public UncFileMapping MapFile(Guid fileId, string fileName)
        {
            return new UncFileMapping("localhost", string.Format(@"\DataFlow\{0}", fileName));
        }
    }
}
