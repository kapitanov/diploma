using System.Windows;
using System.Windows.Controls;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    public class StageDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var window = Application.Current.MainWindow;

            var stage = item as Stage;
            if (stage != null)
            {
                switch (stage.State)
                {
                    case StageState.Pending:
                        return window.FindResource("PendingStageTemplate") as DataTemplate;
                    case StageState.Processing:
                        return window.FindResource("ProcessingStageTemplate") as DataTemplate;
                    case StageState.Completed:
                        return window.FindResource("CompletedStageTemplate") as DataTemplate;
                    case StageState.Failed:
                        return window.FindResource("FailedStageTemplate") as DataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
