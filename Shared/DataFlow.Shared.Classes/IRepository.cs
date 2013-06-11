using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.ServiceModel;
using AISTek.DataFlow.Shared.Classes.Contracts;
using System.IO;

namespace AISTek.DataFlow.Shared.Classes
{
    /// <summary>
    /// Data repository contract
    /// </summary>
    [ContractClass(typeof(ContractForIRepository))]
    public interface IRepository
    {
        /// <summary>
        /// Creates new file.
        /// </summary>
        /// <param name="fileName">
        /// Non-unique name of new file.
        /// </param>
        /// <returns>
        /// GUID of the new file.
        /// </returns>
        Guid CreateNew(string fileName);

        /// <summary>
        /// Deletes the file from repository.
        /// </summary>
        /// <param name="fileUid">
        /// File's GUID.
        /// </param>
        void Delete(Guid fileUid);

        /// <summary>
        /// Browses all the files in repository.
        /// </summary>
        /// <param name="beginFrom">
        /// A zero-based index of element to start from.
        /// </param>
        /// <param name="itemsCount">
        /// An amount of items to show. Must be greater than zero.
        /// </param>
        /// <returns>
        /// A list of <see cref="FileName"/> that contains all available items in the requested range.
        /// </returns>
        IEnumerable<FileName> Browse(int beginFrom, int itemsCount);

        /// <summary>
        /// Uploads file to repository.
        /// </summary>
        /// <param name="fileId">
        /// File's GUID.
        /// </param>
        /// <param name="dataStream">
        /// Data stream.
        /// </param>
        void Upload(Guid fileId, Stream dataStream);

        /// <summary>
        /// Uploads new file to repository.
        /// </summary>
        /// <param name="name">
        /// Name of the new file.
        /// </param>
        /// <param name="dataStream">
        /// Data stream.
        /// </param>
        /// <returns>
        /// New file's GUID.
        /// </returns>
        Guid UploadNew(string name, Stream dataStream);

        /// <summary>
        /// Downloads file from repository.
        /// </summary>
        /// <param name="id">
        /// File's GUID.
        /// </param>
        /// <param name="sharedAccess">
        /// If true, then file will be cached in memory in order to make it available for other processes.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Stream"/> that contains file's data.
        /// </returns>
        Stream Download(Guid id, bool sharedAccess = true);
    }
}
