using System;
using System.IO;
using System.Text;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Util;
using File = AISTek.DataFlow.Shared.Classes.File;

namespace AISTek.DataFlow.MasterNode.Tests.RepositoryTester
{
    public static class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Enter file's size (MB): ");

                int size;
                if (!int.TryParse(Console.ReadLine(), out size))
                    return;

                using (var client = new RepositoryClientFactory().CreateClient())
                {
                    Console.WriteLine("Connecting...");
                    client.Open();

                    for (var i = 0; i < 1; i++)
                    {
                        Stream stream = null;
                        using (stream)
                        {
                            string data;
                            using (Perfomance.Trace("Generating data stream...").BindToConsole())
                            {
                                data = GenerateRandomString(size * 1024 * 1024);
                                stream = WriteString(data);
                            }

                            // Write
                            Guid id;
                            using (Perfomance.Trace("client.Upload").BindToConsole())
                            {
                                id = client.UploadNew("test", stream);
                            }

                            // Read all
                            var link = new FileLink { Id = id };
                            Stream download;
                            using (Perfomance.Trace("client.Download").BindToConsole())
                            {
                                download = client.Download(link.Id);
                            }
                            var readString = ReadString(download);
                            Console.WriteLine(readString == data
                                                  ? "Pass"
                                                  : "Fail");
                        }
                    }
                }
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
            return new MemoryStream(Encoding.ASCII.GetBytes(text));
        }
    }
}
