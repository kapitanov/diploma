using System;
using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public sealed class SchedulerConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(JobPipelineProperty)]
        [ConfigurationCollection(typeof(JobPipelineConfigurationElementCollection))]
        public JobPipelineConfigurationElementCollection JobPipeline
        {
            get { return (JobPipelineConfigurationElementCollection)this[JobPipelineProperty]; }
        }

        [ConfigurationProperty(TaskProviderProperty)]
        public TaskProviderConfigurationSection TaskProvider
        {
            get { return (TaskProviderConfigurationSection)this[TaskProviderProperty]; }
        }

        private const string JobPipelineProperty = "pipeline";
        private const string TaskProviderProperty = "taskProvider";
    }
}