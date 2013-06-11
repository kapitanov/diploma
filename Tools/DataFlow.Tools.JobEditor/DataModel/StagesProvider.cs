using System.Collections.Generic;
using AISTek.DataFlow.Tools.JobEditor.DataModel.Stages;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel
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
                         };
        }

        public IList<Stage> Stages { get; private set; }
    }
}
