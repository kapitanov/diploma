using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AISTek.DataFlow.Threading
{
    public static class TypeSafeThreadPool
    {
        public static void Queue<T>(Action<T> action, T data)
        {
            ThreadPool.QueueUserWorkItem(x => action((T) x), data);
        }
    }
}
