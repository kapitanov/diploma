using System;
using System.Configuration;
using AISTek.DataFlow.ComputeNode.Classes;

namespace AISTek.DataFlow.ComputeNode.Service.Configuration
{
    internal sealed class ExecutionCoreConfiguration : IExecutionCoreConfiguration
    {
        public ExecutionCoreConfiguration()
        {
            configurationSection = ConfigurationManager.GetSection(SectionName)
                    as ComputeNodeConfiguration;
            if (configurationSection == null)
                throw new ConfigurationErrorsException(string.Format(UnableToAccessConfigurationError, SectionName));
        }

        #region Implementation of IExecutionCoreConfiguration

        public Type Sandbox { get { return configurationSection.Sandbox; } }

        #endregion

        private readonly ComputeNodeConfiguration configurationSection;
        private const string SectionName = @"dataflow/computeNode";
        private const string UnableToAccessConfigurationError = "Unable to access configuration section \"{0}\" in web.config file.";
    }
}
