using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Util;
using AISTek.DataFlow.Shared.DataExchange;
using GenericTasks;

namespace AISTek.DataFlow.Sdk.Samples.MapReduce
{
    public sealed class Map : BaseTask
    {
        public override void Execute()
        {
            currentNodeIndex = this.Parameter<int>("MapReduce.MapIndex");
            topItemsCount = this.Parameter<int>("MapReduce.ItemsCount");
            traceMessage = string.Format("AISTek.DataFlow.Sdk.Samples.MapReduce.Map({0})", currentNodeIndex);

            using (Perfomance.BeginTrace(traceMessage)
                .BindToConsole()
                .Start())
            {
                var text = ReadFile(Task.InputFiles[0]);
                List<Word> results;
                using (Perfomance.Trace(traceMessage + ": Build groups").BindToConsole())
                {
                    var words = (from word in Regex.Split(text, @"\W+")
                                 where word.Length > 2
                                 select word)
                                 .ToList();

                    results = (from g in
                                   (from word in words
                                    group word by word into groups
                                    let word = groups.Key
                                    let count = groups.Count()
                                    orderby count descending
                                    select new { Word = word, Count = count })
                               select new Word(g.Word, g.Count / (double)words.Count))
                        .ToList();
                }

                using (Perfomance.Trace(traceMessage + ": Write results").BindToConsole())
                {
                    var resultSet = new Words(results.Take(topItemsCount));
                    resultSet.Serialize().ToExistingFile(Repository, Task.OutputFiles.First());
                }
            }
        }

        private string ReadFile(FileLink link)
        {
            using (Perfomance.Trace(traceMessage + ": Read input file").BindToConsole())
            {
                var stream = Repository.Download(link.Id, false);
                using (var reader = new StreamReader(stream, Encoding.Default, false, 4096))
                {
                    var text = reader.ReadToEnd();
                    return text;
                }
            }
        }

        private int currentNodeIndex;
        private string traceMessage;
        private int topItemsCount;
    }
}
