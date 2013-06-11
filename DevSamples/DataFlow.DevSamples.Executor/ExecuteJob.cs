using System;
using System.Diagnostics;
using System.Threading;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Shared.Classes;
using Japi = AISTek.DataFlow.Shared.Japi;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class ExecuteJob : Stage
    {
        public ExecuteJob()
            : base("Executing job")
        { }

        public override void Execute(
            Japi.Job job,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            var timer = Stopwatch.StartNew();
            jobManager.OpenJob(job.Id);
            jobManager.StartJob();

            while(true)
            {
                var state = jobManager.QueryJobState(job.Id);

                if(state == JobState.Completed)
                {
                    timer.Stop();
                    updateStatus(string.Format("Job completed in {0} ms", timer.ElapsedMilliseconds));
                    return;
                }

                if(state == JobState.Failed)
                {
                    timer.Stop();
                    using (ConsoleMessage.Error())
                    {
                        updateStatus("Job failed. Loading error report...");
                        var summary = jobManager.GetErrorReport(job.Id);
                        updateStatus(string.Format("Error report for job {{{0}, \"{1}\"}}", summary.JobId,
                                                   summary.JobName));
                        foreach (var report in summary.Errors)
                        {
                            updateStatus(string.Format("Task {{{0}, \"{1}\"}}: {2}",
                                                       report.Task.Id, report.Task.Name, report.Error));
                        }
                    }
                    return;
                }

                Thread.Sleep(100);
            }
        }
    }
}
