using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.Shared.Classes.RepositoryService;

namespace AISTek.DataFlow.Shared.Classes.Contracts
{
    [DebuggerStepThrough]
    [ContractClassFor(typeof(IRepositoryService))]
    internal class ContractForIRepositoryService : IRepositoryService
    {
        public RemoteFileInfo CreateNew(string fileName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(fileName));

            throw new NotImplementedException(); 
        }

        public RemoteFileInfo QueryFileInfo(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid fileUid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RemoteFileInfo> Browse(int beginFrom, int itemsCount)
        {
            Contract.Requires<ArgumentException>(beginFrom >= 0);
            Contract.Requires<ArgumentException>(itemsCount > 0);

            throw new NotImplementedException();
        }
    }
}
