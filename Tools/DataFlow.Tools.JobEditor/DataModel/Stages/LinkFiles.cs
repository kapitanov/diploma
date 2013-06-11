using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class LinkFiles : Stage
    {
        public LinkFiles()
            : base("Linking files...")
        {}

        #region Overrides of Stage

        public override void Execute(
            JobDefinition job, 
            Action<int, int> notifyProgress, 
            Action<string> updateStatus,
            IRepository repositoryService, 
            IJobManager jobManager)
        {
            updateStatus("Linking files...");
        }

        #endregion
    }
}
