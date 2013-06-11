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
using System;

namespace AISTek.DataFlow.Sdk.Samples.MapReduce2
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
                var text = ReadFile(Task.InputFiles.First());
                List<Word> results;
                using (Perfomance.Trace(traceMessage + ": Build groups").BindToConsole())
                {
                    results = (from g in
                                   (from word in Regex.Split(text, @"\W+")
                                    where word.Length > 2
                                    group word by word
                                    into groups
                                    let word = groups.Key
                                    let count = groups.Count()
                                    select new {Word = word, Count = count})
                               orderby g.Count descending
                               select new Word(g.Word, g.Count))
                        .ToList();
                }

                using (Perfomance.Trace(traceMessage + ": Write results").BindToConsole())
                {
                    var resultSet = new Words(results.Take(topItemsCount));
                    resultSet.Serialize().ToExistingFile(Repository, Task.OutputFiles.First());
                }
            }
        }

        private string ReadFile(FileLink l)
        {
            Func<FileLink, string> readPath = (link) =>
            {
                using (Perfomance.Trace(traceMessage + ": Read input file").BindToConsole())
                    using (var reader = new StreamReader(Repository.Download(link.Id), Encoding.UTF8, false, 4096))
                        return reader.ReadToEnd();
            };


            return System.IO.File.ReadAllText(readPath(l));
        }
                

        private int currentNodeIndex;
        private string traceMessage;
        private int topItemsCount;
    }
}
