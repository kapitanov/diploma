using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class JobRuntimeCallback : IJobManagerCallback
    {
        public JobRuntimeCallback(JobRuntime runtime, Guid jobId)
        {
            if (runtime == null)
                throw new ArgumentNullException("runtime");

            this.runtime = runtime;
            this.jobId = jobId;
        }

        public void JobCompleted(Job job)
        {
            if (job.Id == jobId)
            {
            }
        }

        public void JobFailed(Job job)
        {
            if (job.Id == jobId)
            {
            }
        }

        private readonly JobRuntime runtime;
        private readonly Guid jobId;
    }
}