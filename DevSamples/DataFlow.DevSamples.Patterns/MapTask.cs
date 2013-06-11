using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Patterns;
using System.Text.RegularExpressions;
using AISTek.DataFlow.PerfomanceToolkit;

namespace AISTek.DataFlow.DevSamples.Patterns
{
    public class MapTask : IMapTask<string, Words>
    {
        public Words Execute(string text)
        {
            using (Perfomance.BeginTrace("IMapTask<string, Words>")
                .BindToConsole()
                .Start())
            {
                var results = (from g in
                                   (from word in Regex.Split(text, @"\W+")
                                    where word.Length > 2
                                    group word by word
                                        into groups
                                        let word = groups.Key
                                        let count = groups.Count()
                                        select new { Word = word, Count = count })
                               orderby g.Count descending
                               select new Word(g.Word, g.Count))
                            .ToList();

                return new Words(results.Take(10));
            }
        }
    }
}
