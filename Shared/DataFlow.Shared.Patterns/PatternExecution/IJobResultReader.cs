﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISTek.DataFlow.Shared.Patterns.PatternExecution
{
    internal interface IJobResultReader
    {
        IJobExecutionResult ReadResult(JobRuntime runtime);
    }
}
