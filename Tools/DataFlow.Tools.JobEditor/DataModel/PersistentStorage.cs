using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using AISTek.DataFlow.Tools.JobEditor.ViewModel;
using AISTek.DataFlow.Util.Linq;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
{
    public class PersistentStorage
    {
        public JobDefinition Load(string path)
        {
            using (var stream = new System.IO.FileInfo(path).OpenRead())
            {
                var job = serializer.Deserialize(stream) as JobDefinition;

                // Create verteces
                job.Graph = new TaskGraph();
                job.Tasks.ForEach(task => job.Graph.AddVertex(new TaskVertex(task)));
 
                // Create edges
                job.Graph.Vertices.ForEach(
                    vertex => vertex.Task.Dependencies.ForEach(
                        task =>
                        {
                            var source = job.Graph.Vertices.First(v => v.Task == task);
                            job.Graph.AddEdge(new TaskEdge(source, vertex));
                        }));

                return job;
            }
        }

        public void Save(JobDefinition job, string path)
        {
            using (var stream = new System.IO.FileInfo(path).OpenWrite())
            {
                serializer.Serialize(stream, job);
            }
        }

        private readonly IFormatter serializer = new BinaryFormatter();
    }
}
