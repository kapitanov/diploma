using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for ClassBrowser.xaml
    /// </summary>
    public partial class ClassBrowser
    {
        public static string Select(string path, Type interfaceType)
        {
            var dialog = new ClassBrowser(path, interfaceType);
            dialog.ShowDialog();
            return dialog.className;
        }

        private ClassBrowser(string path, Type interfaceType)
        {
            InitializeComponent();
            this.path = path;
            this.interfaceType = interfaceType;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
        }

        private readonly string path;
        private readonly Type interfaceType;
        private string className = null;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (listTaskClasses.SelectedItem != null)
            {
                className = (listTaskClasses.SelectedItem as Type).AssemblyQualifiedName;
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            className = null;
            Close();
        }

        private void AeroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            className = null;
            var info = new FileInfo(path);
            if (!info.Exists)
            {
                MessageBox.Show(string.Format("File \"{0}\" doesn't exist!", path), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            try
            {
                var buffer = info.Open(FileMode.Open).ReadToBuffer();
                var assembly = Assembly.Load(buffer);
                listTaskClasses.ItemsSource = from type in assembly.GetExportedTypes()
                                              where (from interf in type.GetInterfaces()
                                                     where interf == interfaceType
                                                     select interf).Count() > 0
                                              select type;

                textBoxAssembly.Text = assembly.FullName;
                textBoxLocalPath.Text = path;
            }
            catch (Exception err)
            {
                MessageBox.Show(string.Format("File \"{0}\" opening error:\n{1}", path, err), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
        }
    }
}
