using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;

namespace AISTek.DataFlow.Tools.LogViewer
{
    public partial class ViewLog
    {
        public ViewLog()
        {
            InitializeComponent();
        }

        internal IViewHost ViewHost { get; set; }

        private IList<Log> logItems;

        internal void LoadData()
        {
            this.Asynch(() =>
            {
                try
                {
                    using (var db = new LogsConnection())
                    {
                        db.Logs.MergeOption = MergeOption.NoTracking;
                        logItems = db.Logs.Include("Categories").Include("Categories").OrderByDescending(item => item.Timestamp).ToList();
                        foreach (var item in logItems)
                        {
                            foreach (var category in item.Categories)
                            {
                                category.CategoryReference.Load();
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, ()=>
            {
                ViewHost.SetStatus("Fetching data...");
                ViewHost.ShowProgressBar();
            }, ViewHost.HideProgressBar, () =>
            {
                grid.ItemsSource = logItems;
                ViewHost.SetStatus(string.Format("{0} items fetched", logItems.Count));
            });
        }

        private void grid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(grid.SelectedItem!=null)
                MainView.ViewEvent.Execute(grid.SelectedItem, this);
        }
    }
}
