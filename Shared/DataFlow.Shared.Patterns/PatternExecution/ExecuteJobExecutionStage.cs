using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AISTek.DataFlow.Shared.Classes;
using System.Diagnostics;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal class ExecuteJobExecutionStage : ExecutionStage
    {
        public ExecuteJobExecutionStage(JobRuntime runtime)
            : base(runtime)
        { }

        public override string Name { get { return "ExecuteJob"; } }

        public override void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            Runtime.Connection.JobManager.OpenJob(Job.Id);
            Runtime.Connection.JobManager.StartJob();

            var isJobExecuted = false;
            var syncObject = new ManualResetEvent(false);
            TimerCallback callback = _ =>
            {
                try
                {
                    var state = Runtime.Connection.JobManager.QueryJobState(Job.Id);
                    switch (state)
                    {
                        case JobState.Completed:
                            isJobExecuted = true;
                            syncObject.Set();
                            break;

                        case JobState.Failed:
                            isJobExecuted = false;
                            syncObject.Set();
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception)
                { }
            };

             
            using (var timer = new Timer(callback, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100)))
            {
                syncObject.WaitOne();
            }

            stopwatch.Stop();

            if (isJobExecuted)
            {
                Runtime.JobStateExecuted(stopwatch.Elapsed);
            }
            else
            {
                var errors = Runtime.Connection.JobManager.GetErrorReport(Job.Id);
                Runtime.JobStateFailed(errors);
            }
        }
    }
}
