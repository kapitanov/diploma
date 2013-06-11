using System;
using System.Collections.Generic;
using System.Linq;
using Dto = AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Japi
{
    [Serializable]
    public class Task
    {
        public Task()
        {
            Name = string.Empty;
            EntryPoint = new EntryPoint();
            Dependencies = new List<Task>();
            InputFiles = new List<File>();
            OutputFiles = new List<File>();
            Parameters = new Dictionary<string, string>();
        }

        public string Name { get; set; }

        public Guid Id { get; set; }

        public EntryPoint EntryPoint { get; set; }

        public List<Task> Dependencies { get; set; }

        public List<File> InputFiles { get; set; }

        public List<File> OutputFiles { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        public Dto.Task ToContract()
        {
            return new Dto.Task
            {
                EntryPoint = EntryPoint.ToContract(),
                Id = Id,
                InputFiles = InputFiles.Select(file => file.Link).ToList(),
                OutputFiles = OutputFiles.Select(file => file.Link).ToList(),
                Inputs = Dependencies.Select(dep => dep.ToContract()).ToList(),
                Name = Name,
                Parameters = Parameters
            };
        }
    }
}
