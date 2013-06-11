using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.Shared.Classes;
using System.IO;

namespace AISTek.DataFlow.ComputeNode.Service.Wrappers
{
    internal class RepositoryWrapper : MarshalByRefObject, IRepository
    {
        #region Constructor

        public RepositoryWrapper(IRepository implementation)
        {
            Contract.Requires(implementation != null);
            this.implementation = implementation;
        }

        #endregion

        #region Private fields

        private readonly IRepository implementation;

        #endregion

        #region Implementation of IRepositoryService

        public Guid CreateNew(string fileName)
        {
            return implementation.CreateNew(fileName);
        }

        public void Delete(Guid fileUid)
        {
            implementation.Delete(fileUid);
        }

        public IEnumerable<FileName> Browse(int beginFrom, int itemsCount)
        {
            return implementation.Browse(beginFrom, itemsCount);
        }

        public void Upload(Guid id, Stream stream)
        {
           implementation.Upload(id, stream);
        }

        public Guid UploadNew(string name, Stream stream)
        {
            return implementation.UploadNew(name, stream);
        }

        /// <summary>
        /// Downloads file form repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// An instance of <see cref="File"/> that contains file's data.
        /// </returns>
        public Stream Download(Guid id, bool sharedAccess)
        {
            var stream = implementation.Download(id, sharedAccess);
            return stream;
        }

        #endregion
    }
}
