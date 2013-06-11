using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Diagnostics.Contracts;
using AISTek.DataFlow.Shared.Classes.Contracts;

namespace AISTek.DataFlow.Shared.Classes.RepositoryService
{
    /// <summary>
    /// Data repository service's contract
    /// </summary>
    [ContractClass(typeof(ContractForIRepositoryService))]
    [ServiceContract(
        Namespace = Namespaces.Repository.Namespace,
        ConfigurationName = Namespaces.Repository.Configuration,
        SessionMode = SessionMode.Allowed)]
    public interface IRepositoryService
    {
        /// <summary>
        /// Creates new file.
        /// </summary>
        /// <param name="fileName">
        /// Non-unique name of new file.
        /// </param>
        /// <returns>
        /// An instance of <see cref="RemoteFileInfo"/> that contains new file's info.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Repository.Actions.CreateNew,
            ReplyAction = Namespaces.Repository.Actions.CreateNewReply)]
        RemoteFileInfo CreateNew(string fileName);

        /// <summary>
        /// Queries file's information.
        /// </summary>
        /// <param name="id">
        /// File's GUID.
        /// </param>
        /// <returns>
        /// An instance of <see cref="RemoteFileInfo"/> that contains file's info.
        /// </returns>
        [OperationContract(
            Action = Namespaces.Repository.Actions.QueryFileInfo,
            ReplyAction = Namespaces.Repository.Actions.QueryFileInfoReply)]
        RemoteFileInfo QueryFileInfo(Guid id);

        /// <summary>
        /// Deletes the file from repository.
        /// </summary>
        /// <param name="fileUid">
        /// File's GUID.
        /// </param>
        [OperationContract(IsOneWay = true, Action = Namespaces.Repository.Actions.Delete)]
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
        /// A list of <see cref="RemoteFileInfo"/> that contains all available items in the requested range.
        /// </returns>
        [OperationContract(
           Action = Namespaces.Repository.Actions.Browse,
           ReplyAction = Namespaces.Repository.Actions.BrowseReply)]
        IEnumerable<RemoteFileInfo> Browse(int beginFrom, int itemsCount);
    }
}
