using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;

namespace AISTek.DataFlow.Tools.RepositoryViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        static MainWindow()
        {
            DeleteFile = new RoutedUICommand("DeleteFile", "DeleteFile", typeof(MainWindow));
        }

        public static RoutedUICommand DeleteFile;

        public MainWindow()
        {
            InitializeComponent();
            HideWait();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.ExtendGlassFrame(new Thickness(-1)))
            {
                Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        private int currentPage = 0;

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            ++currentPage;
            labelCurrentPage.Content = currentPage + 1;
            LoadPage((int)pageSize.SelectedItem, currentPage);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            --currentPage;
            if (currentPage < 0)
                currentPage = 0;
            labelCurrentPage.Content = currentPage + 1;
            LoadPage((int)pageSize.SelectedItem, currentPage);
        }

        private void DeleteFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var fileInfo = e.Parameter as FileInfo;
            if (MessageBox.Show(
                string.Format("Delete file {0} ({1})?", fileInfo.Name, fileInfo.Id),
                "Confirm",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Asynch(() =>
                {
                    var id = fileInfo.Id;
                    using (var client = new RepositoryClientFactory().CreateClient())
                    {
                        client.Open();
                        client.Delete(id);
                        client.Close();
                    }
                }, ShowWait, HideWait);
            }
        }

        private void ShowWait()
        {
            waitScreen.Visibility = Visibility.Visible;
        }

        private void HideWait()
        {
            waitScreen.Visibility = Visibility.Collapsed;
        }

        private void LoadPage(int page, int pageNumber)
        {
            IEnumerable<FileInfo> files = null;

            this.Asynch(() =>
            {
                using (var client = new RepositoryClientFactory().CreateClient())
                {
                    client.Open();
                    files = client.Browse(page * pageNumber, page).Select(x => new FileInfo { Id = x.Id, Name = x.Name });
                    client.Close();
                }
            }, ShowWait, HideWait, () =>
            {
                filesListView.ItemsSource = files;
            });
        }
    }
}


