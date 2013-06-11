using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Tools.Explorer;
using AISTek.DataFlow.Tools.Explorer.JobManager;
using AISTek.DataFlow.Tools.Explorer.ServerStatus;

namespace AISTek.Dataflow.Tools.Explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IJobManagerCallback
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if(this.ExtendGlassFrame(new Thickness(-1)))
            {
                Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Version v = null;
                IEnumerable<Job> jobs = null;
                this.Asynch(() =>
                {
                    using (var jobsListClient = new JobManagerClient(new InstanceContext(this)))
                    {
                        jobsListClient.Open();
                        jobs = jobsListClient.GetAllJobs();
                        jobsListClient.Close();
                    }

                    using (var statusClient = new ServerStatusClient())
                    {
                        statusClient.Open();
                        v = statusClient.GetServerVersion();
                        statusClient.Close();
                    }

                }, ShowProgressBar, HideProgressBar, () =>
                {
                    ToolBarView_Connected(v);
                    treeViewJobs.ItemsSource = jobs;
                });
            }
            catch (Exception error)
            {
                ErrorView.Show(error);
            }
        }

        public void ShowProgressBar()
        {
            this.Synch(() =>
            {
                waitScreen.Visibility = Visibility.Visible;
            });
        }

        public void HideProgressBar()
        {
            this.Synch(() =>
            {
                waitScreen.Visibility = Visibility.Collapsed;
            });
        }

        private void ToolBarView_NotConnected()
        {
            stateConnnected.Visibility = Visibility.Collapsed;
            stateDisconnnected.Visibility = Visibility.Visible;
        }

        private void ToolBarView_Connected(Version version)
        {
            stateConnnected.Visibility = Visibility.Visible;
            stateDisconnnected.Visibility = Visibility.Collapsed;
            stateConnnected.Content = string.Format(Resources[stateConnnected.Name].ToString(), version);
        }

        #region Implementation of IJobManagerCallback

        public void JobCompleted(Job job)
        {
            
        }

        public void JobFailed(Job job)
        {
            
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideProgressBar();
            ToolBarView_NotConnected();
        }

        private void SelectJob_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


