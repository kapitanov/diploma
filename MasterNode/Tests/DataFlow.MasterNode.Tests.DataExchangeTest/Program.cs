using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Shared.DataExchange;
using AISTek.DataFlow.Util;
using File = System.IO.File;

namespace AISTek.DataFlow.MasterNode.Tests.DataExchangeTest
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(args[0]);

            RunExperiment(() => GeneratePlainData(size * 1000));
            RunExperiment(() => GenerateComplexData(size * 100));
        }

        public static void RunExperiment<T>(Func<T> dataGenerator)
            where T : IEquatable<T>
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("Generating data for experiment \"{0}\"...", typeof(T).Name);
            var ram = Environment.WorkingSet;
            var data = dataGenerator();
            ram = Environment.WorkingSet - ram;
            Console.WriteLine("~{0} MB of data.\nStarting experiment \"{1}\"...", ram / 1024 / 1024, typeof(T).Name);
            var experiments = new List<Experiment>();
            var serializations = new[]
                {
                    DataContractSerialization.BinaryFormat,
                    DataContractSerialization.JsonDataContract,
                    DataContractSerialization.XmlDataContract,
                    DataContractSerialization.XmlFormat
                };

            foreach (var serialization in serializations)
            {
                using (var client = new RepositoryClientFactory().CreateClient())
                {
                    client.Open();
                    experiments.Add(
                            3.Times(() => RunTest(client, data, serialization))
                            .Aggregate((x, y) => new Experiment
                            {
                                SerializeTime = x.SerializeTime + y.SerializeTime,
                                DeserializeTime = x.DeserializeTime + y.DeserializeTime
                            })
                            .Divided(3)
                            .Named(serialization.ToString()));
                }
            }

            Console.WriteLine("Experiment \"{0}\" results:", typeof(T).Name);
            foreach (var experiment in experiments)
            {
                Console.WriteLine("{0:32}\t{1}\tms\t{2}\tms",
                    experiment.Name,
                    experiment.SerializeTime.TotalMilliseconds,
                    experiment.DeserializeTime.TotalMilliseconds);
            }
        }

        public static Experiment RunTest<T>(
            IRepository repository,
            T data,
            DataContractSerialization serialization)
            where T : IEquatable<T>
        {
            while (true)
            {
                var experiment = new Experiment();
                Guid id;
                using (Perfomance.Trace(string.Empty)
                    .BindToCallback((_, time) => { experiment.SerializeTime = TimeSpan.FromMilliseconds(time); }))
                {
                    id = data.Serialize(new DataContractOptions(serialization)).ToNewFile(repository, "DEX_Test");
                }


                T readenData;
                using (Perfomance.Trace(string.Empty)
                    .BindToCallback((_, time) => { experiment.DeserializeTime = TimeSpan.FromMilliseconds(time); }))
                {
                    readenData = repository.Deserialize<T>(
                        new FileLink { Id = id },
                        new DataContractOptions(serialization));
                }

                repository.Delete(id);
                if (data.Equals(readenData))
                    return experiment;

                Console.WriteLine("Experiment failed {0}", typeof(T));
            }

            return null;
        }

        public static PlainData GeneratePlainData(int length)
        {
            var random = new Random();

            Func<string> generateString = () =>
            {
                var sb = new StringBuilder();

                for (var i = 0; i < 1024 + 512; ++i)
                {
                    sb.Append((char)random.Next('a', 'z'));
                }
                return sb.ToString();
            };

            return new PlainData(Enumerable.Range(0, length).Select(_ => generateString()));
        }

        public static ComplexData GenerateComplexData(int length)
        {
            var random = new Random();
            Func<string> generateString = () =>
            {
                var sb = new StringBuilder();

                for (var i = 0; i < 128; ++i)
                {
                    sb.Append((char)random.Next('a', 'z'));
                }
                return sb.ToString();
            };

            counter = 0;

            var items = Enumerable.Range(0, length).Select(_ => generateString());
            var result = GenerateComplexDataLevel((int)Math.Ceiling(Math.Log(length, 2)), items);
            Console.WriteLine("{0} leafs generated.", counter);
            return result;
        }

        private static int counter;

        public static ComplexData GenerateComplexDataLevel(int level, IEnumerable<string> leafItems)
        {
            counter += (level % 2 == 0) ? 1 : 0;
            if (level == 0)
            {
                counter++;
                return new LeafComplexData(leafItems);
            }

            return new BranchComplexData(
                GenerateComplexDataLevel(level - 1, leafItems),
                (level % 2 == 0)
                ? new LeafComplexData(leafItems)
                : GenerateComplexDataLevel(level - 1, leafItems));
            //);
        }
    }
}
