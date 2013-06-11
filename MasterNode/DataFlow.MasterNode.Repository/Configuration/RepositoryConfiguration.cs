using System.Configuration;

namespace AISTek.DataFlow.MasterNode.Repository.Configuration
{
    public class RepositoryConfiguration : IRepositoryConfiguration
    {
        public RepositoryConfiguration()
        {
            configurationSection = ConfigurationManager.GetSection(SectionName)
                as RepositoryConfigurationSection;
            if (configurationSection == null)
                throw new ConfigurationErrorsException(string.Format(UnableToAccessConfigurationError, SectionName));
        }

        #region Implementation of ISchedulerConfiguration

        public ICacheConfiguration Cache { get { return configurationSection.Cache; } }

        #endregion

        private readonly RepositoryConfigurationSection configurationSection;
        private const string SectionName = @"dataflow/repository";
        private const string UnableToAccessConfigurationError = "Unable to access configuration section \"{0}\" in web.config file.";
    }
}
