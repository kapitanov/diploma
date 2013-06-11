using System;

namespace AISTek.DataFlow.MasterNode.Setup
{
    public abstract class Deployment
    {
        protected Deployment()
        {
            logger = str => { };
        }

        protected void InitLog(Action<string> logger)
        {
            this.logger = logger;
        }

        protected void WriteToLog(string format, params object[] args)
        {
            logger(string.Format(format, args));
        }

        private Action<string> logger;
    }
}
