using System;

namespace AISTek.DataFlow.Tools.JobEditor.DataModel.Stages
{
    public class StageFailedException : Exception
    {
        public StageFailedException(string message)
            : base(message)
        {}
    }
}
