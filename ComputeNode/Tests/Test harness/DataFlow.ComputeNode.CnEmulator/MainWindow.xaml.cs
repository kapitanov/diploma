using System;
using System.ComponentModel;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;

namespace AISTek.DataFlow.ComputeNode.CnEmulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CloseConnection();
            base.OnClosing(e);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.ExtendGlassFrame(new Thickness(-1)))
            {
                Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        private void Async(Action action, Action syncAction = null)
        {
            Dispatcher.Invoke((Action)(() => waiter.Visibility = Visibility.Visible));
            ThreadPool.QueueUserWorkItem(del =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    SetViewError(e);
                }
                Dispatcher.Invoke((Action)(() =>
                {
                    if (syncAction != null)
                    {
                        try
                        {
                            syncAction();
                        }
                        catch (Exception e)
                        {
                            SetViewError(e);
                        }
                    }
                    waiter.Visibility = Visibility.Hidden;
                }));
            }, action);
        }

        private void Synch(Action action)
        {
            Dispatcher.Invoke(action);
        }

        private void SetViewNoTask()
        {
            Synch(() =>
            {
                viewHasTask.Visibility = Visibility.Collapsed;
                viewNoTask.Visibility = Visibility.Visible;
                treeTask.ItemsSource = new object[] { };
                task = null;
                treeTask.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            });
        }

        private void SetViewHasTask(Task t)
        {
            Synch(() =>
            {
                viewHasTask.Visibility = Visibility.Visible;
                viewNoTask.Visibility = Visibility.Collapsed;
                treeTask.ItemsSource = new[] { t };
                treeTask.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            });
        }

        private void SetViewError(Exception e)
        {
            Synch(() =>
            {
                viewHasTask.Visibility = Visibility.Collapsed;
                viewNoTask.Visibility = Visibility.Visible;
                treeTask.ItemsSource = new[] { e };
                task = null;
                treeTask.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            });
        }

        private string Status { set { status.Content = value; } }

        private ITaskProviderChannel taskProvider;
        private Task task;

        private void PickButton_Click(object sender, RoutedEventArgs e)
        {
            Status = "Picking a task...";
            Async(() =>
            {
                EnsureConnectionOpened();
                task = taskProvider.GetTask();
            }, () =>
            {
                SetViewHasTask(task);
                Status = "";
            });
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Status = "Completing a task...";
            Async(() =>
            {
                taskProvider.CompleteTask(task.Id);
                task = null;
                Synch(() =>
                {
                    Status = "Disconnecting...";
                });
                CloseConnection();
            }, () =>
            {
                SetViewNoTask();
                Status = "";
            });
        }

        private void FailButton_Click(object sender, RoutedEventArgs e)
        {
            Status = "Completing a task...";
            Async(() =>
            {
                taskProvider.FailTask(task.Id, new ErrorReport(task, new Exception("Task failed for no apparent reason."), ErrorSource.Task));
                task = null;
                CloseConnection();
            }, () =>
            {
                SetViewNoTask();
                Status = "";
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title += string.Format(" [v.{0}]", Assembly.GetExecutingAssembly().GetName().Version);
            Status = "Connecting...";
            Async(OpenConnection, () =>
            {
                Status = "";
            });
        }

        private void EnsureConnectionOpened()
        {
            if (taskProvider != null)
                if (taskProvider.State == CommunicationState.Opened)
                    return;

            CloseConnection();
            OpenConnection();
        }

        private void OpenConnection()
        {
            var t = new TaskProviderClientFactory().CreateClient();
            t.Open();
            taskProvider = t;
        }

        private void CloseConnection()
        {
            if (taskProvider != null)
            {
                taskProvider.Close();
            }
        }
    }
}


