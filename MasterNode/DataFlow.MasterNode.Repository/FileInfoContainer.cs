using System;

namespace AISTek.DataFlow.MasterNode.Repository
{
    public sealed class FileInfoContainer
    {
        public FileInfoContainer(
            Guid id,
            string name,
            string host,
            string path)
        {
            Id = id;
            Name = name;
            Host = host;
            Path = path;
        }

        public Guid Id { get; private set; }
        
        public string Name { get; private set; }
        
        public string Host { get; private set; }
        
        public string Path { get; private set; }
    }
}
