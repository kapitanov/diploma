using System.Windows;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for ErrorReportView.xaml
    /// </summary>
    public partial class ErrorReportView
    {
        public static void ShowReport(ErrorSummary errorReport)
        {
            var dialog = new ErrorReportView(errorReport);
            dialog.ShowDialog();
        }

        protected override void OnSourceInitialized(System.EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        private ErrorReportView(ErrorSummary errorReport)
        {
            InitializeComponent();
            ErrorReport = errorReport;
        }

        public static DependencyProperty ErrorReportProperty = DependencyProperty.Register(
            "ErrorReport", typeof(ErrorSummary), typeof(ErrorReportView));

        public ErrorSummary ErrorReport
        {
            get { return (ErrorSummary)GetValue(ErrorReportProperty); }
            private set { SetValue(ErrorReportProperty, value); }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
