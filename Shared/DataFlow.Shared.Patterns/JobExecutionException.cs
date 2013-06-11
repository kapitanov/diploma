using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AISTek.DataFlow.Shared.Classes;

namespace AISTek.DataFlow.Shared.Patterns
{
    [Serializable]
    public class JobExecutionException : ApplicationException
    {
        public JobExecutionException()
        { }

        public JobExecutionException(string message, ErrorSummary errors)
            : base(message)
        {
            Errors = errors;
        }

        public JobExecutionException(string message, Exception inner)
            : base(message, inner)
        { }

        protected JobExecutionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            Errors = (ErrorSummary)info.GetValue(ErrorSummaryKey, typeof(ErrorSummary));
        }

        public override void GetObjectData(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (Errors != null)
            {
                info.AddValue(ErrorSummaryKey, Errors, typeof(ErrorSummary));
            }
        }

        public ErrorSummary Errors { get; private set; }

        private const string ErrorSummaryKey = "_JobErrorSummary";
    }
}
