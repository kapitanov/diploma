using System.Collections.Generic;

namespace AISTek.DataFlow.ComputeNode.ExecutionConsole
{
    public class Command
    {
        public Command(string name, IEnumerable<string> parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public string Name { get; private set; }
        public IEnumerable<string> Parameters { get; private set; }
    }
}
