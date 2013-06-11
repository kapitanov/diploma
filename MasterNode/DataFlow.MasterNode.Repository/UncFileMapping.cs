namespace AISTek.DataFlow.MasterNode.Repository
{
    public sealed class UncFileMapping
    {
        public UncFileMapping(string host, string path)
        {
            Host = host;
            Path = path;
        }

        public string Host { get; private set; }
        
        public string Path { get; private set; }
    }
}
