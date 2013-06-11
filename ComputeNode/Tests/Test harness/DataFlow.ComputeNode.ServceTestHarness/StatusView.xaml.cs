using System;
using System.Windows;
using System.Threading;
using AISTek.DataFlow.ComputeNode.ServiceTestHarness.Status;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
{
    /// <summary>
    /// Interaction logic for StatusView.xaml
    /// </summary>
    public partial class StatusView
    {
        public StatusView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RetreiveStatus();
        }

        private void RetreiveStatus()
        {
            Async(() =>
            {
                var prov = new ServerStatusClient();
                var ver = prov.GetServerVersion().ToString();
                Dispatcher.Invoke((Action)(() => Version.Text = ver));
            });
        }

        private void Async(Action action)
        {
            Dispatcher.Invoke((Action)(() => waiter.Visibility = Visibility.Visible));
            ThreadPool.QueueUserWorkItem(del =>
            {
                try
                {
                    del.As<Action>()();
                }
                catch (Exception e)
                {
                    Dispatcher.Invoke((Action)(() => ErrorView.Show(e)));
                }
                Dispatcher.Invoke((Action)(() => waiter.Visibility = Visibility.Hidden));
            }, action);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RetreiveStatus();
        }
    }
}


