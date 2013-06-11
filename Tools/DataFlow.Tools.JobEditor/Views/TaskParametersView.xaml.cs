using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Tools.JobEditor.DataModel;
using AISTek.DataFlow.Util.Linq;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for TaskParametersView.xaml
    /// </summary>
    public partial class TaskParametersView
    {
        public static Dictionary<string, string> Edit(Dictionary<string, string> values)
        {
            var dialog = new TaskParametersView(values);
            dialog.ShowDialog();
            return dialog.values;
        }

        private TaskParametersView(Dictionary<string, string> values)
        {
            InitializeComponent();
            this.values = values;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
        }

        private Dictionary<string, string> values;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            values = new Dictionary<string, string>();
            foreach (Pair item in grid.Items)
            {
                values.Add(item.Key, item.Value);
            }

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            values = null;
            Close();
        }

        private void AeroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            values.ForEach(x => grid.Items.Add(new Pair(x.Key, x.Value)));
        }

        private void AddParameter_Click(object sender, RoutedEventArgs e)
        {
            grid.Items.Add(new Pair("New parameter", "New parameter' value"));
        }

        private void RemoveParameter_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItems.Count > 0)
            {
                grid.SelectedItems.OfType<object>().ForEach(grid.Items.Remove);
            }
        }
    }
}
