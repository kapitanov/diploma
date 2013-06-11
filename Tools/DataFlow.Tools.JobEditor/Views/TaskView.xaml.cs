using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Tools.JobEditor.DataModel;
using AISTek.DataFlow.Util.Linq;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView
    {
        private TaskView(TaskRequest task, List<FileRequest> files, List<TaskRequest> tasks)
        {
            InitializeComponent();
            this.task = task;
            this.files = files;
            this.tasks = tasks;
        }

        public static TaskRequest CreateNew(List<FileRequest> files, List<TaskRequest> tasks)
        {
            var dialog = new TaskView(new TaskRequest(), files, tasks);
            dialog.ShowDialog();

            return dialog.task;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        private TaskRequest task;
        private EntryPointDefinition entryPoint;
        private readonly List<FileRequest> files;
        private readonly List<TaskRequest> tasks;
        private Dictionary<string, string> parameters;

        #region Helper methods

        private static void RemoveSelectedItems(ListBox listBox)
        {
            if (listBox.SelectedItems.Count > 0)
            {
                var enumerable = listBox.SelectedItems.Cast<object>().ToList();
                enumerable.ForEach(file => listBox.Items.Remove(file));
            }
        }

        #endregion

        private void AddInputFile_Click(object sender, RoutedEventArgs e)
        {
            var file = AddFileView.Select(files, f => f, canCreate: false);
            if (file != null)
                listInputFiles.Items.Add(file);
        }

        private void RemoveInputFile_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedItems(listInputFiles);
        }

        private void AddOutputFile_Click(object sender, RoutedEventArgs e)
        {
            var file = AddFileView.Select(files, f => f.OfType<CreateFileRequest>().Cast<FileRequest>(), canUpload: false);
            if (file != null)
                listOutputFiles.Items.Add(file);
        }

        private void RemoveOutputFile_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedItems(listOutputFiles);
        }

        private void AddDependency_Click(object sender, RoutedEventArgs e)
        {
            var task = AddDependencyView.Select(tasks);
            if (task != null)
                listDependencies.Items.Add(task);
        }

        private void RemoveDependency_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedItems(listDependencies);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = task.Name;
            textBoxEntrypoint.Text = task.EntryPoint.ToString();

            entryPoint = task.EntryPoint;

            task.Dependencies.ForEach(x => listDependencies.Items.Add(x));
            task.InputFiles.ForEach(x => listInputFiles.Items.Add(x));
            task.OutputFiles.ForEach(x => listOutputFiles.Items.Add(x));

            parameters = task.Parameters;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            task.Name = textBoxName.Text;

            task.EntryPoint = entryPoint;

            task.Dependencies.Clear();
            listDependencies.Items.OfType<TaskRequest>().ForEach(task.Dependencies.Add);

            task.InputFiles.Clear();
            listInputFiles.Items.OfType<FileRequest>().ForEach(task.InputFiles.Add);

            task.OutputFiles.Clear();
            listOutputFiles.Items.OfType<FileRequest>().ForEach(task.OutputFiles.Add);

            task.Parameters = parameters;

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            task = null;
            Close();
        }

        private void EditEntryPoint_Click(object sender, RoutedEventArgs e)
        {
            var ep = EntryPointView.Edit(entryPoint, files);
            if (ep != null)
            {
                entryPoint = ep;
                textBoxEntrypoint.Text = entryPoint.ToString();
            }
        }

        private void EditParameters_Click(object sender, RoutedEventArgs e)
        {
            var p = TaskParametersView.Edit(parameters);
            if (p != null)
                parameters = p;
        }
    }

}
