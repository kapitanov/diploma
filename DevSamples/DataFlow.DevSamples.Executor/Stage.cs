using System;
using AISTek.DataFlow.Shared.Classes;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public abstract class Stage
    {
        protected Stage(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public abstract void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager);
    }
}
