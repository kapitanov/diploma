using System;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorView.Show(e.ExceptionObject as Exception);
        }
    }
}


