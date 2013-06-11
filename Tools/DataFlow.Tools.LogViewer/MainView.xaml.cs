using System.Linq;
using System.Windows;
using System.Windows.Input;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.Tools.LogViewer
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : IViewHost
    {
        static MainView()
        {
            LoadData = new RoutedUICommand("LoadData", "LoadData", typeof(MainView));
            ViewEvent = new RoutedUICommand("ViewEvent", "ViewEvent", typeof(MainView));
            ViewLogs = new RoutedUICommand("ViewLogs", "ViewLogs", typeof(MainView));
            ClearLogs = new RoutedUICommand("ClearLogs", "ClearLogs", typeof(MainView));
        }

        public MainView()
        {
            InitializeComponent();
            defaultView = new ViewLog { ViewHost = this };
            ViewmodeLogs();
        }

        #region Commands

        public static RoutedUICommand LoadData { get; private set; }

        public static RoutedUICommand ViewEvent { get; private set; }

        public static RoutedUICommand ViewLogs { get; private set; }

        public static RoutedUICommand ClearLogs { get; private set; }

        #endregion

        private void ViewmodeLogs()
        {
            viewList.Visibility = Visibility.Visible;
            viewItem.Visibility = Visibility.Collapsed;
            container.Child = defaultView;
        }

        private void ViewmodeEvent(Log item)
        {
            viewList.Visibility = Visibility.Collapsed;
            viewItem.Visibility = Visibility.Visible;
            container.Child = new ViewLogItem(item);
        }

        #region Implementation of IViewHost

        public void ShowProgressBar()
        {
            this.Synch(() => 
            {
                progressBar.Visibility = Visibility.Visible;
                busy.Visibility = Visibility.Visible; 
            });
        }

        public void HideProgressBar()
        {
            this.Synch(() =>
            {
                progressBar.Visibility = Visibility.Collapsed;
                busy.Visibility = Visibility.Collapsed;
            });
        }

        public void SetStatus(string statusText)
        {
            this.Synch(() => status.Text = statusText);
        }

        #endregion

        private readonly ViewLog defaultView;

        private void ViewEvent_Executed(object _, ExecutedRoutedEventArgs e)
        {
            ViewmodeEvent(e.Parameter as Log);
        }

        private void ViewLogs_Executed(object _, ExecutedRoutedEventArgs e)
        {
            ViewmodeLogs();
        }

        private void Window_Loaded(object _, RoutedEventArgs e)
        {
            LoadData.Execute(null, defaultView);
        }

        private void LoadData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            defaultView.LoadData();
        }

        private void ClearLogs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(MessageBox.Show("Clear all logs?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using(var db = new LogsConnection())
                {
                    db.ClearLogs();
                    db.SaveChanges();
                }

                LoadData.Execute(null, this);
            }
        }
    }
}
