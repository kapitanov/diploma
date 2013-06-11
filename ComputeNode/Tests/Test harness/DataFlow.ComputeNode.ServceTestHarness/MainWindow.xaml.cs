using System.Windows;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
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

        private void OpenTaskView_Click(object sender, RoutedEventArgs e)
        {
            new TaskView().Show();
        }

        private void OpenJobView_Click(object sender, RoutedEventArgs e)
        {
            new JobView().Show();
        }

        private void ServerStatus_Click(object sender, RoutedEventArgs e)
        {
            new StatusView().Show();
        }

        private void Repository_Click(object sender, RoutedEventArgs e)
        {
            new RepositoryView().Show();
        }
    }
}


