using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.ServiceModel;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Util;
using Microsoft.Practices.Unity;
using System.Diagnostics;
using AISTek.DataFlow.Shared.Classes.RepositoryService;

namespace AISTek.DataFlow.MasterNode.Repository
{
    /// <summary>
    /// An implementation of <see cref="IRepositoryService"/> service contract.
    /// </summary>
    [ServiceBehavior(
        ConfigurationName = Namespaces.Repository.Configuration,
        InstanceContextMode = InstanceContextMode.PerCall,
        AutomaticSessionShutdown = true)]
    public class RepositoryService : IRepositoryService
    {
        [InjectionConstructor]
        public RepositoryService(
            [Dependency]
            IRepository repository,
            [Dependency]
            IFileClassesConverter converter)
        {
            Contract.Requires(repository != null);
            Contract.Requires(converter != null);
            Contract.Ensures(this.repository != null);
            Contract.Ensures(this.converter != null);

            this.repository = repository;
            this.converter = converter;
        }

        #region Private fields

        private readonly IRepository repository;
        private readonly IFileClassesConverter converter;

        #endregion

        #region Implementation of IRepositoryService

        /// <summary>
        /// Creates new file.
        /// </summary>
        /// <param name="fileName">
        /// Non-unique name of new file.
        /// </param>
        /// <returns>
        /// An instance of <see cref="RemoteFileInfo"/> that contains new file's info.
        /// </returns>
        public RemoteFileInfo CreateNew(string fileName)
        {
            using (Perfomance.Trace("RepositoryService::CreateNew(\"{0}\")", fileName).BindToTrace())
            {
                return Service.Shield(() =>
                {
                    var info = repository.CreateFile(fileName);
                    return new RemoteFileInfo
                    {
                        Id = info.Id,
                        Name = info.Name,
                        Uri = MakeUri(info.Host, info.Path)
                    };
                });
            }
        }

        /// <summary>
        /// Queries file's information.
        /// </summary>
        /// <param name="id">
        /// File's GUID.
        /// </param>
        /// <returns>
        /// An instance of <see cref="RemoteFileInfo"/> that contains file's info.
        /// </returns>
        public RemoteFileInfo QueryFileInfo(Guid id)
        {
            using (Perfomance.Trace("RepositoryService::QueryFileInfo(\"{0}\")", id).BindToTrace())
            {
                return Service.Shield(() =>
                {
                    var info = repository.QueryFile(id);
                    return new RemoteFileInfo
                    {
                        Id = info.Id,
                        Name = info.Name,
                        Uri = MakeUri(info.Host, info.Path)
                    };
                });
            }
        }

        /// <summary>
        /// Deletes the file from repository.
        /// </summary>
        /// <param name="fileUid">
        /// File's GUID.
        /// </param>
        public void Delete(Guid fileUid)
        {
            using (Perfomance.Trace("RepositoryService::Delete(\"{0}\")", fileUid).BindToTrace())
            {
                Service.Shield(() => repository.DeleteFile(fileUid));
            }
        }

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
        public IEnumerable<RemoteFileInfo> Browse(int beginFrom, int itemsCount)
        {
            using (Perfomance.Trace("RepositoryService::Browse({0}, {1})", beginFrom, itemsCount).BindToTrace())
            {
                return Service.Shield(() => from info in repository.BrowseFiles(beginFrom, itemsCount)
                                            select new RemoteFileInfo
                                            {
                                                Id = info.Id,
                                                Name = info.Name,
                                                Uri = MakeUri(info.Host, info.Path)
                                            });
            }
        }

        #endregion

        private static Uri MakeUri(string host, string path)
        {
            return new Uri(string.Format(@"\\{0}{1}", host, path));
        }
    }
}
