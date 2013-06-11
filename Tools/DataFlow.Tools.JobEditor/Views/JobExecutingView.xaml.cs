using System;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for JobExecutingView.xaml
    /// </summary>
    public partial class JobExecutingView
    {
        public JobExecutingView(string title)
        {
            InitializeComponent();
            Title = title;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        public event EventHandler OnCancelled;

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            buttonCancel.Content = "Cancelling...";
            buttonCancel.IsEnabled = false;
            if (OnCancelled != null)
                OnCancelled(this, e);
        }
    }
}
