using System.Collections.Generic;
using System.Linq;
using AISTek.DataFlow.PerfomanceToolkit;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.DataExchange;
using GenericTasks;

namespace AISTek.DataFlow.Sdk.Samples.HashDistribute
{
    public sealed class TakeTopList : BaseTask
    {
        public override void Execute()
        {
            takeTop = this.Parameter<int>("TakeTop");
            var traceMessage = "AISTek.DataFlow.Sdk.Samples.HashDistribute.TakeTopList()";

            using (Perfomance.BeginTrace(traceMessage)
                .BindToConsole()
                .Start())
            {
                var groups = new List<Word>();
                using (Perfomance.Trace(traceMessage + ": Read data").BindToConsole())
                {
                    foreach (var words in from file in Task.InputFiles
                                          let wordsContainer = Repository.Deserialize<Words>(file)
                                          select wordsContainer)
                    {
                        groups.AddRange(words);
                    }
                }

                List<Word> resultGroups;
                using (Perfomance.Trace(traceMessage + ": Build list").BindToConsole())
                {
                    resultGroups = (from grp in
                                        (from g in groups
                                         group g by g.Value
                                             into grp
                                             let count = grp.Sum(x => x.Count)
                                             let word = grp.Key
                                             select new Word(word, count))
                                    orderby grp.Count descending
                                    select grp)
                    .Take(takeTop)
                    .ToList();
                }
                using (Perfomance.Trace(traceMessage + ": Write result").BindToConsole())
                {
                    var result = new Words(resultGroups);
                    result.Serialize().ToExistingFile(Repository, Task.OutputFiles.First());
                }
            }
        }

        private int takeTop;
    }
}
