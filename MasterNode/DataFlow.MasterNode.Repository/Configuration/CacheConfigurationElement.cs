using System.ComponentModel;
using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Repository.Configuration
{
    public sealed class CacheConfigurationElement : ConfigurationElement, ICacheConfiguration
    {
        [ConfigurationProperty(EnabledProperty, IsRequired = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool Enabled
        {
            get { return (bool)this[EnabledProperty]; }
            set { this[EnabledProperty] = value; }
        }

        [ConfigurationProperty(CacheSizeProperty, IsRequired = true)]
        [TypeConverter(typeof(Int32Converter))]
        public int CacheSize
        {
            get { return (int)this[CacheSizeProperty]; }
            set { this[CacheSizeProperty] = value; }
        }

        private const string EnabledProperty = "enabled";
        private const string CacheSizeProperty = "cacheSize";
    }
}
