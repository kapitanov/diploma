using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;
using File = AISTek.DataFlow.Shared.Classes.File;
using FileInfo = AISTek.DataFlow.Shared.Classes.FileInfo;

namespace AISTek.DataFlow.ComputeNode.GeneralEngineTestHarness
{
    internal class RepositoryMock : MarshalByRefObject, IRepository
    {
        public Guid CreateNew(string fileName)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid fileUid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileName> Browse(int beginFrom, int itemsCount)
        {
            throw new NotImplementedException();
        }

        public void Upload(Guid fileId, Stream dataStream)
        {
            throw new NotImplementedException();
        }

        public Guid UploadNew(string name, Stream dataStream)
        {
            throw new NotImplementedException();
        }

        public Stream Download(Guid id, bool sharedAccess)
        {
            throw new NotImplementedException();
        }
    }
}
