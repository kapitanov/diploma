using System;
using System.IO;
using System.Linq;
using System.Text;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Util;
using File = System.IO.File;

namespace AISTek.DataFlow.MasterNode.Tests.TextReadWriteTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var bufferSizes = Enumerable.Range(0, 16).Select(n => (int)Math.Pow(2, n + 10));
            Console.WriteLine("Enter path to text file:");
            var path = Console.ReadLine();
            string text;
            using (var reader = File.OpenText(path))
            {
                text = reader.ReadToEnd();
            }

            using (var client = new RepositoryClientFactory().CreateClient())
            {
                var stream = new MemoryStream(Encoding.Default.GetBytes(text));
                var id = client.UploadNew("text_upload_test", stream);
                foreach (var bufferSize in bufferSizes)
                {
                    var readenText = ReadFile(client, id, bufferSize);
                    if(!text.Equals(readenText))
                        Console.WriteLine("Failed.");
                }
            }
        }

        private static string ReadFile(IRepository repository, Guid id, int bufferSize)
        {
            using (Perfomance.Trace("Download with buffer size {0} Kbytes", bufferSize / 1024).BindToConsole())
            {
                using (var reader = new StreamReader(repository.Download(id), Encoding.Default, false, bufferSize))
                {
                    var text = reader.ReadToEnd();
                    return text;
                }
            }
        }
    }
}
