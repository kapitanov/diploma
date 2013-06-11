using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public sealed class SchedulerConfiguration : ISchedulerConfiguration
    {
        public SchedulerConfiguration()
        {
            configurationSection = ConfigurationManager.GetSection(SectionName)
                as SchedulerConfigurationSection;
            if (configurationSection == null)
                throw new ConfigurationErrorsException(string.Format(UnableToAccessConfigurationError, SectionName));
        }

        #region Implementation of ISchedulerConfiguration

        public IJobPipelineConfiguration JobPipeline { get { return configurationSection.JobPipeline; } }

        public TaskProviderConfigurationSection TaskProvider { get { return configurationSection.TaskProvider; } }

        #endregion

        private readonly SchedulerConfigurationSection configurationSection;
        private const string SectionName = @"dataflow/scheduler";
        private const string UnableToAccessConfigurationError = "Unable to access configuration section \"{0}\" in web.config file.";
    }
}
