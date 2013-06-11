using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.MasterNode.Repository
{
    public class InMemoryRepository : IRepository
    {
        public InMemoryRepository(IUncFileMapper fileMapper)
        {
            FileMapper = fileMapper;
            Console.WriteLine("InMemoryRepository started");
        }
        
        public IUncFileMapper FileMapper { get; private set; }

        public FileInfoContainer CreateFile(string name)
        {
            var id = Guid.NewGuid();
            var mapping = FileMapper.MapFile(id, name);

            var file = new FileInfoContainer(id, name, mapping.Host, mapping.Path);
            lock (this)
            {
                files.Add(id, file);
            }

            return file;
        }

        public FileInfoContainer QueryFile(Guid id)
        {
            return files[id];
        }

        public void DeleteFile(Guid id)
        {
            files.Remove(id);
        }

        public IEnumerable<FileInfoContainer> BrowseFiles(int startFrom, int itemsCount)
        {
            lock (this)
            {
                return files.OrderBy(x => x.Value.Name)
                    .Select(x => x.Value)
                    .Skip(startFrom)
                    .Take(itemsCount)
                    .ToList();
            }
        }

        private Dictionary<Guid, FileInfoContainer> files = new Dictionary<Guid, FileInfoContainer>();
    }
}
