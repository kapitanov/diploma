using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Tools.JobEditor.DataModel;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for EntryPointView.xaml
    /// </summary>
    public partial class EntryPointView
    {
        public static EntryPointDefinition Edit(
            EntryPointDefinition entryPointDefinition,
            IList<FileRequest> files)
        {
            var dialog = new EntryPointView(entryPointDefinition, files);
            dialog.ShowDialog();
            return dialog.entryPoint;
        }

        private EntryPointView(EntryPointDefinition entryPoint, IList<FileRequest> files)
        {
            InitializeComponent();
            this.entryPoint = entryPoint;
            this.files = files;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        private EntryPointDefinition entryPoint;
        private readonly IList<FileRequest> files;
        private FileRequest mainAssembly;
        private string taskClassName;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            entryPoint.Assembly = mainAssembly;
            entryPoint.ClassName = taskClassName;
            entryPoint.References = listReferences.Items.OfType<FileRequest>().ToList();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            entryPoint = null;
            Close();
        }

        private void SelectAssembly_Click(object sender, RoutedEventArgs e)
        {
            var file = AddFileView.Select(files, list => list.OfType<UploadFileRequest>().Cast<FileRequest>(), canCreate: false);
            if (file != null)
            {
                mainAssembly = file;
                textBoxMainAssembly.Text = mainAssembly.ToString();
            }
        }

        private void SelectClass_Click(object sender, RoutedEventArgs e)
        {
            var className = ClassBrowser.Select((mainAssembly as UploadFileRequest).Path, typeof (ITask));
            if (className != null)
            {
                taskClassName = className;
                textBoxTaskClass.Text = taskClassName;
            }
        }

        private void AddRef_Click(object sender, RoutedEventArgs e)
        {
            var file = AddFileView.Select(files, list => list.OfType<UploadFileRequest>().Cast<FileRequest>(), canCreate: false);
            if (file != null)
            {
                listReferences.Items.Add(file);
            }
        }

        private void RemoveRef_Click(object sender, RoutedEventArgs e)
        {
            if (listReferences.SelectedItem != null)
                listReferences.Items.Remove(listReferences.SelectedItem);
        }

        private void AeroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainAssembly = entryPoint.Assembly;
            taskClassName = entryPoint.ClassName;

            entryPoint.References.ForEach(x => listReferences.Items.Add(x));
            textBoxTaskClass.Text = taskClassName;
            textBoxMainAssembly.Text = mainAssembly.ToString();
        }
    }
}
