using AISTek.DataFlow.ComputeNode.Classes;
using AISTek.DataFlow.Shared.Classes;

namespace GenericTasks
{
    public abstract class BaseTask : ITask
    {
        public IRepository Repository { get; set; }

        public Task Task { get; set; }

        public abstract void Execute();

        public void Teardown()
        { }

        public bool Validate(ISystemResources resources)
        {
            return true;
        }
    }
}
