using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal enum JobExecutionState
    {
        /// <summary>
        /// Job state is unknown or job doesn't exist yet
        /// </summary>
        Indeterminate,

        /// <summary>
        /// Job has been failed to execute
        /// </summary>
        Faulted,

        /// <summary>
        /// Job has been executed successfully, but the results are not read yet
        /// </summary>
        Executed,

        /// <summary>
        /// Job has been executed successfully, the results are read
        /// </summary>
        Completed
    }
}
