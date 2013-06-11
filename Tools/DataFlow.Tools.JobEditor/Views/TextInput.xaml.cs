using System;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for TextInput.xaml
    /// </summary>
    public partial class TextInput
    {
        public static string Enter(string title, string value = null)
        {
            var dialog = new TextInput(title, value);
            dialog.ShowDialog();
            return dialog.text;
        }

        private TextInput(string title, string value)
        {
            InitializeComponent();
            Title = title;
            textBox.Text = value ?? string.Empty;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
        }

        private string text = null;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            text = textBox.Text;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            text = null;
            Close();
        }
    }
}
