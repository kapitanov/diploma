using System.Collections.Generic;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Tools.JobEditor.DataModel;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for AddDependencyView.xaml
    /// </summary>
    public partial class AddDependencyView
    {
        public static TaskRequest Select(IEnumerable<TaskRequest> tasks)
        {
            var dialog = new AddDependencyView(tasks);
            dialog.ShowDialog();
            return dialog.result;
        }

        private AddDependencyView(IEnumerable<TaskRequest> tasks)
        {
            InitializeComponent();
            listTasks.ItemsSource = tasks;
        }

        protected override void OnSourceInitialized(System.EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        private TaskRequest result = null;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(listTasks.SelectedItem != null)
            {
                result = listTasks.SelectedItem as TaskRequest;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            result = null;
            Close();
        }
    }
}


