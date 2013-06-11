namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public interface ISchedulerConfiguration
    {
        IJobPipelineConfiguration JobPipeline { get; }

        TaskProviderConfigurationSection TaskProvider { get; }
    }
}
