using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Tools.JobEditor.DataModel;
using AISTek.DataFlow.Tools.JobEditor.ViewModel;
using AISTek.DataFlow.Util;
using Microsoft.Win32;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IJobManagerCallback
    {
        public MainWindow()
        {
            InitializeComponent();
            NewJob(this, null);
        }

        public JobDefinition Job { get; private set; }
        private string fileName;
        private readonly PersistentStorage storage = new PersistentStorage();

        private void AddTask(object sender, ExecutedRoutedEventArgs e)
        {
            TaskView.CreateNew(Job.Files, Job.Tasks).IfNotNull(
                task =>
                {
                    Job.Tasks.Add(task);
                    var vertex = new TaskVertex(task);
                    Job.Graph.AddVertex(vertex);

                    foreach (var target in task.Dependencies.Select(
                        dependency => Job.Graph.Vertices.First(x => x.Task == dependency)))
                    {
                        Job.Graph.AddEdge(new TaskEdge(target, vertex));
                    }
                });
        }

        private void Exit(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void EditJobName(object sender, ExecutedRoutedEventArgs e)
        {
            Views.TextInput.Enter("Enter job's name:", Job.Name).IfNotNull(name =>
            {
                Job.Name = name;
                textBoxJobName.Text = name;
            });
        }

        private void ExecuteJob(object sender, ExecutedRoutedEventArgs e)
        {
            new UploadJobView(new StagesProvider(), Job, new RepositoryClientFactory(), new JobManagerClientFactory().CreateClient(this)).ShowDialog();
        }

        private void OpenJob(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "Jobs|*.dfjob" };
            if (dialog.ShowDialog() == true)
            {
                fileName = dialog.FileName;
                Job = storage.Load(fileName);
                AssingJob();
            }
        }

        private void NewJob(object sender, ExecutedRoutedEventArgs e)
        {
            if(Job != null)
            {
                if(MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            fileName = null;
            Job = new JobDefinition();
            AssingJob();
        }

        private void AssingJob()
        {
            graphLayout.Graph = Job.Graph;
            textBoxJobName.Text = Job.Name;
        }

        private void DiscardJob()
        {
            graphLayout.Graph = null;
        }

        private void SaveJob(object sender, ExecutedRoutedEventArgs e)
        {
            if (fileName == null)
            {
                SaveJobAs(sender, e);
                return;
            }

            //DiscardJob();
            storage.Save(Job, fileName);
            //AssingJob();
        }

        private void SaveJobAs(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "Jobs|*.dfjob" };
            if (dialog.ShowDialog() == true)
            {
                fileName = dialog.FileName;
                SaveJob(sender, e);
            }
        }

        #region Implementation of IJobManagerCallback

        /// <summary>
        /// Notifies client that specified job has been completed.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Shared.Classes.Job"/> that has been completed.
        /// </param>
        public void JobCompleted(Job job)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Notifies client that specified job has been failed.
        /// </summary>
        /// <param name="job">
        /// An instance of <see cref="Shared.Classes.Job"/> that has been failed.
        /// </param>
        public void JobFailed(Job job)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}


