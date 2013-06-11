using System;
using System.Collections.Generic;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public interface IJobPipelineConfiguration
    {
        IEnumerable<Type> JobBuildingStrategies { get; }
    }
}
