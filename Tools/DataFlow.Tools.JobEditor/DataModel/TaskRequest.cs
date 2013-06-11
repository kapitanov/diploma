using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    [Serializable]
    public class TaskRequest
    {
        public TaskRequest()
        {
            Name = string.Empty;
            EntryPoint = new EntryPointDefinition();
            Dependencies = new List<TaskRequest>();
            InputFiles = new List<FileRequest>();
            OutputFiles = new List<FileRequest>();
            Parameters = new Dictionary<string, string>();
        }

        public string Name { get; set; }

        public Guid Id { get; set; }

        public EntryPointDefinition EntryPoint { get; set; }

        public List<TaskRequest> Dependencies { get; private set; }

        public List<FileRequest> InputFiles { get; private set; }

        public List<FileRequest> OutputFiles { get; private set; }

        public Dictionary<string, string> Parameters { get; set; }

        public Task ToContract()
        {
            return new Task
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
