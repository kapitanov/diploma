using System;

namespace AISTek.DataFlow.Shared.Classes
{
    public static class TaskExtensions
    {
        public static T Parameter<T>(this ITask task, string parameter)
        {
            if(!task.Task.Parameters.ContainsKey(parameter))
                throw new Exception("Parameter is not defined");

            return (T)Convert.ChangeType(task.Task.Parameters[parameter], typeof(T));
        }
    }
}
