using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Tools.JobEditor.DataModel;
using Microsoft.Win32;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for AddFileView.xaml
    /// </summary>
    public partial class AddFileView
    {
        public static FileRequest Select(
            IList<FileRequest> allFiles,
            Func<IEnumerable<FileRequest>, IEnumerable<FileRequest>> filter,
            bool canCreate = true,
            bool canUpload = true)
        {
            var dialog = new AddFileView(allFiles, filter, canCreate, canUpload);
            dialog.ShowDialog();
            return dialog.result;
        }

        private AddFileView(
            IList<FileRequest> allFiles,
            Func<IEnumerable<FileRequest>, IEnumerable<FileRequest>> filter,
            bool canCreate,
            bool canUpload)
        {
            InitializeComponent();
            this.allFiles = allFiles;
            this.filter = filter;
            buttonCreateFile.Visibility = canCreate
                                              ? Visibility.Visible
                                              : Visibility.Collapsed;
            buttonUploadFile.Visibility = canUpload
                                              ? Visibility.Visible
                                              : Visibility.Collapsed;

            UpdateFilesList();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        private FileRequest result = null;
        private readonly IList<FileRequest> allFiles;
        private readonly Func<IEnumerable<FileRequest>, IEnumerable<FileRequest>> filter;

        private void UpdateFilesList()
        {
            listFiles.ItemsSource = filter(allFiles);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (listFiles.SelectedItem != null)
            {
                result = listFiles.SelectedItem as FileRequest;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            var fileName = Views.TextInput.Enter("Enter new file's name:");
            if (fileName != null)
            {
                var newFile = new CreateFileRequest(fileName);
                allFiles.Add(newFile);
                UpdateFilesList();
            }
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var newFile = new UploadFileRequest(dialog.FileName);
                allFiles.Add(newFile);
                UpdateFilesList();
            }
        }

        private void listFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OkButton_Click(this, e);
        }
    }
}


