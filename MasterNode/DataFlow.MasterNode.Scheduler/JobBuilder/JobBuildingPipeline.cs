using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using AISTek.DataFlow.MasterNode.DataModel;
using AISTek.DataFlow.MasterNode.Scheduler.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

namespace AISTek.DataFlow.MasterNode.Scheduler.JobBuilder
{
    public class JobBuildingPipeline : IJobBuildingPipeline
    {
        public JobBuildingPipeline(
            [Dependency]
            IUnityContainer unityContainer,
            [Dependency]
            ISchedulerConfiguration schedulerConfiguration)
        {
            Contract.Requires(unityContainer != null);
            Contract.Requires(schedulerConfiguration != null);
            Contract.Requires(schedulerConfiguration.JobPipeline != null);
            Contract.Requires(schedulerConfiguration.JobPipeline.JobBuildingStrategies != null);

            using (new Tracer("Initialization of job building pipeline"))
            {
                Logger.Write(
                    LogCategories.Information(
                        "Loading job building strategies...",
                        LogCategories.TaskServices,
                        LogCategories.JobBuildingPipeline));

                var strategies = from strategy in schedulerConfiguration.JobPipeline.JobBuildingStrategies
                                 let _ = LogLoadingStrategy(strategy)
                                 let obj = CreateStrategyInstance(unityContainer, strategy) 
                                 where obj != null
                                 select obj as IJobBuildStrategy;
                Strategies = strategies.ToList();

                Logger.Write(
                   LogCategories.Information(
                       string.Format("Completed, {0} strategies) loaded.", Strategies.Count),
                       LogCategories.TaskServices,
                       LogCategories.JobBuildingPipeline));
            }
        }

        #region Implementation of IJobBuildingPipeline

        /// <summary>
        /// Gets a mutable list of all availible build strategies.
        /// </summary>
        public IList<IJobBuildStrategy> Strategies { get; private set; }

        /// <summary>
        /// Builds the job on job start event.
        /// </summary>
        /// <param name="job">
        /// Job to build.
        /// </param>
        public void Build(Job job)
        {
            foreach (var strategy in Strategies)
            {
                Logger.Write(
                   LogCategories.Information(string.Format("Perfoming {0} on {1}", strategy.GetType().Name, job),
                       LogCategories.TaskServices,
                       LogCategories.TaskProviderService));

                strategy.Build(job);
            }
        }

        #endregion

        private static Type LogLoadingStrategy(Type type)
        {
            Contract.Requires(type != null);
            Logger.Write(
                    LogCategories.Information(
                        string.Format("Strategy \"{0}\"", type.FullName),
                        LogCategories.TaskServices,
                        LogCategories.JobBuildingPipeline));
            return type;
        }

        private static object CreateStrategyInstance(IUnityContainer container, Type type)
        {
            try
            {
                var instance = container.Resolve(type);
                if (instance == null)
                    throw new Exception("Unable to create instance of build strategy.");

                return instance;
            }
            catch (Exception e)
            {
                Logger.Write(
                    LogCategories.Error(
                        string.Format("Strategy \"{0}\" wasn't loaded due to the following error:\n{1}", 
                            type.FullName, e),
                        LogCategories.TaskServices,
                        LogCategories.JobBuildingPipeline));
                return null;
            }
        }

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(Strategies != null);
        }
    }
}


