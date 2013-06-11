using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
{
    /// <summary>
    /// Interaction logic for JobView.xaml
    /// </summary>
    public partial class JobView : IJobManagerCallback
    {
        public JobView()
        {
            InitializeComponent();
        }

        private Job currentJob;
        private IJobManager jobManager;

        #region Async actions

        protected void ShowWait()
        {
            Dispatcher.Invoke((Action)(() => waiter.Visibility = Visibility.Visible));
        }

        protected void HideWait()
        {
            Dispatcher.Invoke((Action)(() => waiter.Visibility = Visibility.Hidden));
        }

        private void Async(Action action, Action syncAction = null)
        {
            ShowWait();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Dispatcher.Invoke((Action)(() => ErrorView.Show(e)));
                }

                Dispatcher.Invoke((Action)(() =>
                {
                    if (syncAction != null)
                        syncAction();
                    waiter.Visibility = Visibility.Hidden;
                }));
            });
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                //context = new InstanceContext(this);
                //jobManager = new  (context);
                jobManager = new JobManagerClientFactory().CreateClient(this);
                HideWait();
            });
        }

        private void CreateJob_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                currentJob = jobManager.CreateJob("Test Job");
            }, () =>
            {
                treeView.ItemsSource = new[] { currentJob };
            });
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                var t = new Task
                            {
                                Name = "test task",
                                EntryPoint = new EntryPoint { QualifiedClassName = "System.Int32" },
                                InputFiles = new List<FileLink> { new FileLink
                                                                      {
                                                                          Metadata = new Dictionary<string, string>{{"Offset", "0"}}
                                                                      }, },
                                Inputs = new List<Task>(),
                                OutputFiles = new List<FileLink>{ new FileLink
                                                                      {
                                                                          Metadata = new Dictionary<string, string>{{"Offset", "120"}}
                                                                      }, },
                                Outputs = new List<Task>(),
                                Parameters = new Dictionary<string, string>
                                                 {
                                                     {"Core.UseSandbox", true.ToString()},
                                                     {"RepositoryClient.Login","testUser"},
                                                     {"RepositoryClient.Password","test"},
                                                     {"Core.AllowInteraction", false.ToString()},
                                                     {"Core.WorkerThreads", 4.ToString()}
                                                 }
                            };
                t.Id = jobManager.CreateTask(t);
                currentJob.Tasks.Add(t);
            }, () =>
            {
                treeView.ItemsSource = new[] { currentJob };
            });
        }

        private void StartJob_Click(object sender, RoutedEventArgs e)
        {
            Async(() => jobManager.StartJob());
        }

        #region Implementation of IJobManagerCallback

        public void JobCompleted(Job job)
        {
            MessageBox.Show("Job {{{0}}} has been successfully executed!".FormatWith(job.Id));
            //popupCompleted.IsOpen = true;
        }


        public void JobFailed(Job job)
        {
            MessageBox.Show("Job {{{0}}} failed to execute!".FormatWith(job.Id));
            //popupCompleted.IsOpen = true;
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popupCompleted.IsOpen = false;
        }

        private void CreateTasks_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                // T1 => {T2 | (T3 => T4)}
                var t1 = CreateTask(1);
                var t2 = CreateTask(2);
                var t3 = CreateTask(3);
                var t4 = CreateTask(4);

                // T3 => T4
              //  t3.Outputs.Add(t4);
                t4.Inputs.Add(t3);

                // T1 => T2
               // t1.Outputs.Add(t2);
                t2.Inputs.Add(t1);

                // T1 => T3
              //  t1.Outputs.Add(t3);
                t3.Inputs.Add(t1);

                // Register tasks
                RegisterTask(t1);
                RegisterTask(t2);
                RegisterTask(t3);
                RegisterTask(t4);

                currentJob.Tasks.Add(t1);
                currentJob.Tasks.Add(t2);
                currentJob.Tasks.Add(t3);
                currentJob.Tasks.Add(t4);
            }, () =>
            {
                treeView.ItemsSource = new[] { currentJob };
            });
        }

        private Task CreateTask(int index)
        {
            return new Task
                {
                    Name = "Task #{0}".FormatWith(index),
                    EntryPoint = new EntryPoint { QualifiedClassName = "System.Int32" },
                    InputFiles = new List<FileLink> { new FileLink
                                                                      {
                                                                          Metadata = new Dictionary<string, string>{{"Offset", (index * 128 + 64).ToString()}}
                                                                      }, },
                    Inputs = new List<Task>(),
                    OutputFiles = new List<FileLink>{ new FileLink
                                                                      {
                                                                          Metadata = new Dictionary<string, string>{{"Offset", "128"}}
                                                                      }, },
                    Outputs = new List<Task>(),
                    Parameters = new Dictionary<string, string>
                                                 {
                                                     {"Core.UseSandbox", true.ToString()},
                                                     {"RepositoryClient.Login","testUser"},
                                                     {"RepositoryClient.Password","test"},
                                                     {"Core.AllowInteraction", false.ToString()},
                                                     {"Core.WorkerThreads", 4.ToString()},
                                                     {"Tasks.Index", index.ToString()}
                                                 }
                };
        }

        private void RegisterTask(Task task)
        {
            task.Id = jobManager.CreateTask(task);
        }
    }
}


