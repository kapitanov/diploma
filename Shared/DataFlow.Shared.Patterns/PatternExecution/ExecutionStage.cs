using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal abstract class ExecutionStage
    {
        protected ExecutionStage(JobRuntime runtime)
        {
            this.runtime = runtime;
        }

        public abstract string Name { get; }

        protected Japi.Job Job { get { return runtime.Job; } }

        protected JobRuntime Runtime { get { return runtime; } }

        public abstract void Execute();

        private readonly JobRuntime runtime;
    }
}
