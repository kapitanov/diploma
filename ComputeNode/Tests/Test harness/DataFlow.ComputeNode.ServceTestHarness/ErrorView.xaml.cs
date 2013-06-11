using System;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
{
    /// <summary>
    /// Interaction logic for ErrorView.xaml
    /// </summary>
    public partial class ErrorView
    {
        public static void Show(Exception e)
        {
            var dialog = new ErrorView
                             {
                                 errorType = { Text = e.GetType().FullName },
                                 errorMessage = { Text = e.Message },
                                 errorStackTrace = { Text = e.StackTrace }
                             };
            dialog.Show();
        }

        private ErrorView()
        {
            InitializeComponent();
        }
    }
}


