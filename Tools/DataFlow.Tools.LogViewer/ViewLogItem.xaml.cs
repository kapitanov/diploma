using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace AISTek.DataFlow.Tools.LogViewer
{
    /// <summary>
    /// Interaction logic for ViewLogItem.xaml
    /// </summary>
    public partial class ViewLogItem
    {
        public ViewLogItem(Log item)
        {
            InitializeComponent();
            Item = item;

            var items = new List<DisplayItem>
                            {
                                new DisplayItem("Title", item.Title),
                                new DisplayItem("Id", item.Id),
                                new DisplayItem("Event Id", item.EventId),
                                new DisplayItem("Priority", item.Priority),
                                new DisplayItem("Severity", item.Severity),
                                new DisplayItem("Timestamp", item.Timestamp),
                                new DisplayItem("Category", ConvertCategories(item)),
                                new DisplayItem("Message", item.Message),
                                new DisplayItem("Machine", item.MachineName),
                                new DisplayItem("App. domain", item.AppDomainName),
                                new DisplayItem("Process", string.Format("\"{0}\" (#{1})", item.ProcessName, item.ProcessId)),
                                new DisplayItem("Thread", string.Format("\"{0}\" (#{1})", item.ThreadName, item.Win32ThreadId)),
                                new DisplayItem("Title", item.Title)
                            };

            Items = items;

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(item.Message);
                xmlViewTab.Visibility=Visibility.Visible;
                xmlTreeView.ItemsSource = new[] {xmlDocument};
            }
            catch (Exception)
            {
                xmlViewTab.Visibility = Visibility.Collapsed;
            }
            
        }

        public static DependencyProperty ItemProperty = DependencyProperty.Register(
            "Item", typeof(Log), typeof(ViewLogItem));

        public Log Item
        {
            get { return (Log)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public static DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(IEnumerable<DisplayItem>), typeof(ViewLogItem));

        public IEnumerable<DisplayItem> Items
        {
            get { return (IEnumerable<DisplayItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        
        public class DisplayItem
        {
            public DisplayItem(string name, object value)
            {
                Name = name;
                Value = value.ToString();
            }

            public string Name { get; private set; }

            public string Value { get; private set; }
        }

        private static string ConvertCategories(Log item)
        {
            var sb = new StringBuilder();
            var categories = item.Categories.ToArray();
            for (var i = 0; i < categories.Length; i++)
            {
                if (categories[i].Category!=null)
                    sb.Append(categories[i].Category.Name);
                else
                    sb.Append("?");
                if (i < categories.Length - 1)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
    }
}
