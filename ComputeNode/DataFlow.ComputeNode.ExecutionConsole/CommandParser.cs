using System;
using System.Linq;

namespace AISTek.DataFlow.ComputeNode.ExecutionConsole
{
    public class CommandParser
    {
        public Command Parse(string input)
        {
            var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Command(parts.First().ToLowerInvariant(), parts.Skip(1));
        }
    }
}
