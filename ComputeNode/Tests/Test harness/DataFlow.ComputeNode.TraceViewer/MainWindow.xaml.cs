using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AISTek.DataFlow.Util;
using AISTek.DataFlow.Util.Linq;

namespace DataFlow.ComputeNode.TraceViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Async(() =>
            {
                using (var client = new Status.ServerStatusClient())
                {
                    var debug = client.GetAllDebugEvents();
                    Events = debug.OrderBy(item => item.TimeStamp).ToList();
                }
            }, () =>
            {
                dataRows.Rows.Clear();
                Events.ForEach(item =>
                {
                    var row = new TableRow();
                    row.Cells.Add(
                        new TableCell(new Paragraph(new Run(item.TimeStamp.ToString("hh:mm:ss"))))
                            {
                                BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                                BorderThickness = new Thickness(1)
                            });
                    var para = new Paragraph();
                    var first = true;
                    foreach (var line in item.Text.Split('\n'))
                    {
                        if (first)
                            first = false;
                        else
                            para.Inlines.Add(new LineBreak());
                        para.Inlines.Add(new Run(line));
                    }
                    row.Cells.Add(
                        new TableCell(para)
                        {
                            BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                            BorderThickness = new Thickness(1)
                        });
                    dataRows.Rows.Add(row);
                });
            });
        }

        public IEnumerable<Status.Event> Events { get; private set; }

        private void Async(Action action, Action syncAction = null)
        {
            Dispatcher.Invoke((Action)(() => waiter.Visibility = Visibility.Visible));
            ThreadPool.QueueUserWorkItem(del =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                { }
                Dispatcher.Invoke((Action)(() =>
                {
                    if (syncAction != null)
                        syncAction();
                    waiter.Visibility = Visibility.Hidden;
                }));
            }, action);
        }
    }
}
