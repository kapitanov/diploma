using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace AISTek.DataFlow.MasterNode.Repository
{
    public class Repository : IRepository
    {
        public Repository(IUncFileMapper uncFileMapper)
        {
            this.uncFileMapper = uncFileMapper;
        }

        #region Implementation of IRepository

        public FileInfoContainer CreateFile(string name)
        {
            return DataContextOperation(name, (fileName, db) =>
            {
                var guid = Guid.NewGuid();
                var mapping = uncFileMapper.MapFile(guid, fileName);
                var file = new FileEntity
                {
                    Id = guid,
                    Name = fileName,
                    Host = mapping.Host,
                    Path = mapping.Path
                };
                db.AddToFiles(file);
                db.SaveChanges();

                return new FileInfoContainer(file.Id, file.Name, file.Host, file.Path);
            });
        }

        public FileInfoContainer QueryFile(Guid id)
        {
            return DataContextOperation(id, (fileId, db) =>
            {
                var file = db.Files.Where(f => f.Id == fileId)
                    .ToList()
                    .Select(f => new FileInfoContainer(f.Id, f.Name, f.Host, f.Path))
                    .FirstOrDefault();
                if (file == null)
                    throw new FileNotFoundException();

                return file;
            });
        }

        public void DeleteFile(Guid id)
        {
            DataContextOperation(id, (fileId, db) =>
            {
                var file = db.Files.Where(f => f.Id == fileId).FirstOrDefault();
                if (file == null)
                    throw new FileNotFoundException();
                db.DeleteObject(file);
                db.SaveChanges();
            });
        }

        public IEnumerable<FileInfoContainer> BrowseFiles(int startFrom, int itemsCount)
        {
            return DataContextOperation(
                new { StartFrom = startFrom, ItemsCount = itemsCount },
                (p, db) => db.Files
                    .OrderBy(f => f.Id)
                    .Skip(p.StartFrom)
                    .Take(p.ItemsCount)
                    .ToList()
                    .Select(f => new FileInfoContainer(f.Id, f.Name, f.Host, f.Path)));
        }

        #endregion

        private Entities CreateDataContext()
        {
            return new Entities(ConnectionString);
        }

        private string ConnectionString
        {
            get
            {
                lock (this)
                {
                    if (connectionString == null)
                    {
                        var format = ConfigurationManager.ConnectionStrings["Repository Entities Connection"].ConnectionString;
                        var physConnection = ConfigurationManager.ConnectionStrings["Repository SQL Server"];
                        connectionString = string.Format(format, physConnection.ProviderName, physConnection.ConnectionString);
                    }
                }

                return connectionString;
            }
        }

        private string connectionString = null;
        private readonly IUncFileMapper uncFileMapper;

        private T DataContextOperation<TParams, T>(TParams parameters, Func<TParams, Entities, T> func)
        {
            using (var dataContext = CreateDataContext())
            {
                return func(parameters, dataContext);
            }
        }

        private void DataContextOperation<TParams>(TParams parameters, Action<TParams, Entities> func)
        {
            using (var dataContext = CreateDataContext())
            {
                func(parameters, dataContext);
            }
        }
    }
}
