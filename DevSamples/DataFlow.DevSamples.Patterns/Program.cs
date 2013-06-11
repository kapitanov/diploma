using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Shared.DataExchange;
using AISTek.DataFlow.Shared.Patterns;

namespace AISTek.DataFlow.DevSamples.Patterns
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var files = new List<string>();
                string str;
                do
                {
                    Console.Write(">");
                    str = Console.ReadLine();
                    if (System.IO.File.Exists(str))
                    {
                        files.Add(str);
                    }
                    else if (!string.IsNullOrEmpty(str))
                    {
                        Console.WriteLine("File doesn't exist");
                    }

                } while (!string.IsNullOrEmpty(str));

                var fileData = files.Select(System.IO.File.ReadAllText).ToList();

                foreach (var serialization in new[]
                    { 
                        DataContractSerialization.BinaryFormat,
                        DataContractSerialization.JsonDataContract,
                        DataContractSerialization.XmlDataContract,
                        DataContractSerialization.XmlFormat
                    })
                {
                    try
                    {
                        RunMapReduce(fileData, serialization);
                    }
                    catch
                    { }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void RunMapReduce(IEnumerable<string> fileData, DataContractSerialization serialization)
        {
            using (var connection = DataFlowConnection.CreateDefaultConnection())
            {
                Console.WriteLine();
                Console.WriteLine("Serialization: {0}", serialization);
                var timer = Stopwatch.StartNew();
                var result = MapReduce.Create<string, Words, Words>(connection, new DataContractOptions(serialization))
                        .Map<MapTask>()
                        .Reduce<ReduceTask>()
                        .Create()
                        .Execute(fileData);

                try
                {
                    var value = result.Value;
                    timer.Stop();

                    const string format = @"{0, -30}{1, -30}";

                    Console.WriteLine(format, "Word:", "Frequency:");
                    foreach (var word in value)
                    {
                        Console.WriteLine(format, word.Value, word.Count);
                    }

                    Console.WriteLine("Timing:");
                    Console.WriteLine("Outer: {0}", timer.Elapsed);
                    Console.WriteLine("Inner: {0}", result.Timing);
                }
                catch (JobExecutionException e)
                {
                    Console.WriteLine(e.Message);

                    foreach (var error in e.Errors.Errors)
                    {
                        Console.WriteLine(error.Error);
                    }
                }
            }
        }
    }
}
