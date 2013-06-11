using System.Windows;
using System.Windows.Controls;
using AISTek.DataFlow.Tools.JobEditor.DataModel;

namespace AISTek.DataFlow.Tools.JobEditor.ViewModel
{
    public class FileRequestTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var window = Application.Current.MainWindow;

            if (item is CreateFileRequest)
                return window.FindResource("CreateFileTemplate") as DataTemplate;

            if (item is UploadFileRequest)
                return window.FindResource("UploadFileTemplate") as DataTemplate;


            return base.SelectTemplate(item, container);
        }
    }
}
