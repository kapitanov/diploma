using System.Collections.Generic;

namespace AISTek.DataFlow.DevSamples.Executor
{
    public class StagesProvider
    {
        public StagesProvider()
        {
            Stages = new List<Stage>
                         {
                             new ValidateGraph(),
                             new BuildFileList(),
                             new UploadFiles(),
                             new LinkFiles(),
                             new CreateJob(),
                             new UploadTasks(),
                             new ExecuteJob(),
                             new SaveResults(),
                             new DeleteJob(),
                             new DeleteFiles(),
                         };
        }

        public IEnumerable<Stage> Stages { get; private set; }
    }
}
