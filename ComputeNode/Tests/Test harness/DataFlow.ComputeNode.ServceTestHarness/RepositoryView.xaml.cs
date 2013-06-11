using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Util;
using File = AISTek.DataFlow.Shared.Classes.File;

namespace AISTek.DataFlow.ComputeNode.ServiceTestHarness
{
    /// <summary>
    /// Interaction logic for RepositoryView.xaml
    /// </summary>
    public partial class RepositoryView
    {
        public RepositoryView()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs _)
        {
            try
            {
                using (var client = new RepositoryClientFactory().CreateClient())
                {
                    client.Open();

                    for (var i = 0; i < 5; i++)
                    {

                        var data = GenerateRandomString(16 * 1024 * 1024);

                        // Create
                        var id = client.CreateNew("testFile");
                        var link = new FileLink { Id = id };

                        // Write
                        using (var stream = WriteString(data))
                        {
                            Action<string, long> onCompleted =
                                (msg, ms) => MessageBox.Show(string.Format("{0} in {1} ms", msg, ms));
                            using (Perfomance.Trace("client.Upload").BindToCallback(onCompleted))
                            {
                                client.Upload(link.Id, stream);
                            }

                            // Read all
                            Stream download;
                            using (Perfomance.Trace("client.Download").BindToCallback(onCompleted))
                            {
                                download = client.Download(link.Id);
                            }
                            MessageBox.Show(ReadString(download) == data ? "Pass" : "Fail",
                                            "All file");
                        }
                    }
                }
            }
            catch (WebException e)
            {
                var data = e.Response.GetResponseStream().ReadToBuffer();
                var str = Encoding.Default.GetString(data);
                MessageBox.Show(str);
                ErrorView.Show(e);
            }
        }

        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            var sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next('A', 'Z'));
            }

            return sb.ToString();
        }

        public static string ReadString(Stream stream)
        {
            return Encoding.ASCII.GetString(stream.ReadToBuffer());
        }

        public static Stream WriteString(string text)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(text));
        }

        private void CreateWrongNamedButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
