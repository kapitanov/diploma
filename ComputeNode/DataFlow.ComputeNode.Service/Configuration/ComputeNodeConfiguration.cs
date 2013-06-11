using System;
using System.ComponentModel;
using System.Configuration;

namespace AISTek.DataFlow.ComputeNode.Service.Configuration
{
    public sealed class ComputeNodeConfiguration : ConfigurationSection
    {
        [ConfigurationProperty(SandBoxProperty, IsRequired = true)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type Sandbox
        {
            get { return (Type) this[SandBoxProperty]; }
        }

        private const string SandBoxProperty = "sandbox";
    }
}
