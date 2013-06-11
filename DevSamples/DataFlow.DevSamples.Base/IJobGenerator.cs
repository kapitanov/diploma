using AISTek.DataFlow.Shared.Japi;
namespace AISTek.DataFlow.DevSamples.Base
{
    public interface IJobGenerator
    {
        void Initialize();
        Job CreateJob();
    }
}
