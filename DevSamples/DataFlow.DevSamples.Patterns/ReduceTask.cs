using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Patterns;
using AISTek.DataFlow.PerfomanceToolkit;

namespace AISTek.DataFlow.DevSamples.Patterns
{
    public class ReduceTask : IReduceTask<Words, Words>
    {
        public Words Execute(IGather<Words> partialResults)
        {
            using (Perfomance.BeginTrace("IReduceTask<Words, Words>")
                .BindToConsole()
                .Start())
            {
                var result = (from grp in
                                  (from g in partialResults.SelectMany(w => w)
                                   group g by g.Value
                                       into grp
                                       let count = grp.Sum(x => x.Count)
                                       let word = grp.Key
                                       select new Word(word, count))
                              orderby grp.Count descending
                              select grp)
                                       .Take(10)
                                       .ToList();

                return new Words(result);
            }
        }
    }
}
