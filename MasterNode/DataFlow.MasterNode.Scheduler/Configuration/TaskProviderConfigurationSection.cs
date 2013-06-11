using System;
using System.ComponentModel;
using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public class TaskProviderConfigurationSection : ConfigurationElement, ITaskProviderConfiguration
    {
        [ConfigurationProperty(PingTimeoutProperty, IsRequired = true)]
        [TypeConverter(typeof(InfiniteTimeSpanConverter))]
        public TimeSpan PingTimeout
        {
            get { return (TimeSpan)this[PingTimeoutProperty]; }
            set { this[PingTimeoutProperty] = value; }
        }

        private const string PingTimeoutProperty = "pingTimeout";
    }
}
