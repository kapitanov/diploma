using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class MapReduceJobResultReader<TOutput> : IJobResultReader
    {
        public IJobExecutionResult ReadResult(JobRuntime runtime)
        {
            var resultFile = runtime.Job.Files.First(file => file.Name == Constants.FileName_Result);
            var result = runtime.Connection.Repository.Deserialize<TOutput>(resultFile.Link, runtime.DtoOptions);

            return new MapReduceJobExecutionResult<TOutput>(result, (runtime.ExecutionResult as IntermediateJobExecutionResult).Timing);
        }
    }
}
