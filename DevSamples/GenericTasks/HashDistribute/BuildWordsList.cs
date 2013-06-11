using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using GenericTasks;
using AISTek.DataFlow.Shared.DataExchange;

namespace AISTek.DataFlow.Sdk.Samples.HashDistribute
{
    public sealed class BuildWordsList : BaseTask
    {
        public override void Execute()
        {
            nextNodesCount = this.Parameter<int>("Layer.NextSize");
            var nodeId = this.Parameter<int>("Node");
            traceMessage = string.Format("AISTek.DataFlow.Sdk.Samples.HashDistribute.BuildWordsList({0})", nodeId);

            using (Perfomance.BeginTrace(traceMessage)
                .BindToConsole()
                .Start())
            {
                var text = ReadFile(Task.InputFiles.First());
                List<Group> wordGroups;

                using (Perfomance.Trace(traceMessage + ": Build groups").BindToConsole())
                {
                    wordGroups = (from word in Regex.Split(text, @"\W+")
                                  where word.Length > 2
                                  group word by word
                                  into groups
                                  let word = groups.Key
                                  let count = groups.Count()
                                  let node = NodeHash(word, nextNodesCount)
                                  select new Group {Word = word, Node = node, Count = count})
                        .ToList();
                }

                using (Perfomance.Trace(traceMessage + ": Write outputs").BindToConsole())
                {
                    for (var i = 0; i < nextNodesCount; i++)
                    {
                        var index = i;
                        var link = Task.OutputFiles.First(x => x.Metadata["Node"] == index.ToString());
                        var words = new Words(from wordGroup in wordGroups
                                              where wordGroup.Node == index
                                              select new Word(wordGroup.Word, wordGroup.Count));
                        words.Serialize().ToExistingFile(Repository, link);
                    }
                }
            }
        }

        private string ReadFile(FileLink link)
        {
            using (Perfomance.Trace(traceMessage + ": Read input file").BindToConsole())
            {
                using (var reader = new StreamReader(Repository.Download(link.Id) ,Encoding.Default, false, 4096))
                {
                    var text = reader.ReadToEnd();
                    return text;
                }
            }
        }

        private static int NodeHash(IEnumerable<char> word, int nodes)
        {
            return word.First() % nodes;
        }

        private string traceMessage;
        private int nextNodesCount;

        private class Group
        {
            public string Word { get; set; }
            public int Count { get; set; }
            public int Node { get; set; }
        }
    }
}
