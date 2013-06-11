using System;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public interface ITaskProviderConfiguration
    {
        TimeSpan PingTimeout { get; }
    }
}
