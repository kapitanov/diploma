using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Classes.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(IRepository))]
    internal class ContractForIRepository : IRepository
    {
        Guid IRepository.CreateNew(string fileName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(fileName));

            throw new NotImplementedException();
        }

        IEnumerable<FileName> IRepository.Browse(int beginFrom, int itemsCount)
        {
            Contract.Requires<ArgumentException>(beginFrom >= 0);
            Contract.Requires<ArgumentException>(itemsCount > 0);
            Contract.Ensures(Contract.Result<IEnumerable<FileName>>() != null);

            throw new NotImplementedException();
        }

        public void Upload(Guid fileId, Stream dataStream)
        {
            Contract.Requires<ArgumentNullException>(dataStream != null);

            throw new NotImplementedException();
        }

        public Guid UploadNew(string name, Stream dataStream)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name));
            Contract.Requires<ArgumentNullException>(dataStream != null);

            throw new NotImplementedException();
        }

        public Stream Download(Guid id, bool sharedAccess)
        {
            Contract.Ensures(Contract.Result<Stream>() != null);

            throw new NotImplementedException();
        }

        public void Delete(Guid fileUid)
        {
            throw new NotImplementedException();
        }
    }
}
