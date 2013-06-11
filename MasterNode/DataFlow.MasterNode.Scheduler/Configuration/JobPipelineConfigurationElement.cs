using System;
using System.ComponentModel;
using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public sealed class JobPipelineConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Source type.
        /// </summary>
        [ConfigurationProperty(TypeProperty, IsRequired = true)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type Type
        {
            get { return (Type)this[TypeProperty]; }
            set { this[TypeProperty] = value; }
        }

        private const string TypeProperty = "type";
    }
}
