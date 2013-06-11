using System;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView
    {
        public TaskView()
        {
            InitializeComponent();
        }

        private void PickTask_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                task = taskProvider.GetTask();
            }, () =>
            {
                treeView.ItemsSource = new[] { task };
                ViewMode_HasTask();
            });
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                taskProvider.CompleteTask(task.Id);
                taskProvider = new TaskProviderClientFactory().CreateClient();
            });
            ViewMode_NoTask();
        }

        private void FailTask_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                taskProvider.FailTask(task.Id, new ErrorReport(task, new Exception("Task failed without any reason."), ErrorSource.Task));
            });
            ViewMode_NoTask();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                taskProvider = new TaskProviderClientFactory().CreateClient();

                Dispatcher.Invoke((Action)(() =>
                {
                    splash.Visibility = Visibility.Hidden;
                    ViewMode_NoTask();
                }));
            });
        }

        private void Async(Action action, Action syncAction = null)
        {
            Dispatcher.Invoke((Action)(() => splash.Visibility = Visibility.Visible));
            ThreadPool.QueueUserWorkItem(del =>
            {
                try
                {
                    del.As<Action>()();
                }
                catch (Exception e)
                {
                    Dispatcher.Invoke((Action)(() => ErrorView.Show(e)));
                }
                Dispatcher.Invoke((Action)(() =>
                {
                    if (syncAction != null)
                        syncAction();
                    splash.Visibility = Visibility.Hidden;
                }));
            }, action);
        }

        private void ViewMode_NoTask()
        {
            toolbarNoTask.Visibility = Visibility.Visible;
            toolbarHasTask.Visibility = Visibility.Collapsed;
        }

        private void ViewMode_HasTask()
        {
            toolbarNoTask.Visibility = Visibility.Collapsed;
            toolbarHasTask.Visibility = Visibility.Visible;
        }

        private ITaskProvider taskProvider;
        private Task task;
    }
}


