using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Repository.Configuration
{
    public sealed class RepositoryConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(CacheProperty)]
        public CacheConfigurationElement Cache
        {
            get { return (CacheConfigurationElement)this[CacheProperty]; }
        }

        private const string CacheProperty = "cache";
    }
}
