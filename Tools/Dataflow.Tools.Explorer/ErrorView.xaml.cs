using System;
using System.Windows;
using System.Windows.Media;
using AISTek.DataFlow.PresentationExtensions;

namespace AISTek.DataFlow.Tools.Explorer
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.ExtendGlassFrame(new Thickness(-1)))
            {
                Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }

        private ErrorView()
        {
            InitializeComponent();
        }
    }
}
