namespace AISTek.DataFlow.MasterNode.Repository.Configuration
{
    public interface ICacheConfiguration
    {
        bool Enabled { get; set; }

        int CacheSize { get; set; }
    }
}
