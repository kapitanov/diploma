using System;
using System.Threading;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Tools.JobEditor.Views;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class ExecuteJob : Stage
    {
        public ExecuteJob()
            : base("Executing job...")
        { }

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job,
            Action<int, int> notifyProgress,
            Action<string> updateStatus,
            IRepository repositoryService,
            IJobManager jobManager)
        {
            updateStatus("Executing job...");
            JobExecutingView dialog = null;

            Owner.Synch(() => dialog = new JobExecutingView(string.Format("Executing job {0}...", job)));


            jobManager.OpenJob(job.Id);
            jobManager.StartJob();

            var isWorking = true;

            dialog.OnCancelled += (s, e) =>
            {
                lock (this)
                {
                    if (!isWorking)
                        return;
                    isWorking = false;
                }

                jobManager.CancelJob();
            };
            Owner.Synch(() => dialog.Show());

            var state = default(JobState);
            while (isWorking)
            {
                state = jobManager.QueryJobState(job.Id);
                if (state == JobState.Completed ||
                   state == JobState.Failed)
                {
                    lock (this)
                    {
                        lock (this)
                            isWorking = false;
                        break;
                    }
                }

                Thread.Sleep(100);
            }
            Owner.Synch(() => dialog.Close());
            if (state == JobState.Completed)
            {
                return;
                // MessageBox.Show(string.Format("Job {0} completed!", job));
            }
            if (state == JobState.Failed)
            {
                var errorReport = jobManager.GetErrorReport(job.Id);
                Owner.Synch(() => ErrorReportView.ShowReport(errorReport));
                throw new StageFailedException("Job failed!");
                //MessageBox.Show(string.Format("Job {0} failed!", job));
            }
        }

        #endregion
    }
}
