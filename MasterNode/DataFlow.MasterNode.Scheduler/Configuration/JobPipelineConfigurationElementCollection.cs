using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AISTek.DataFlow.MasterNode.Scheduler.Configuration
{
    public sealed class JobPipelineConfigurationElementCollection : ConfigurationElementCollection, IJobPipelineConfiguration
    {
        #region Overrides of ConfigurationElementCollection

        /// <summary>
        /// При переопределении в производном классе создает новый элемент <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// Новый объект <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new JobPipelineConfigurationElement();
        }

        /// <summary>
        /// При переопределении в производном классе возвращает ключ указанного элемента конфигурации.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Object"/>, используемый в качестве ключа для указанного элемент <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        /// <param name="element">Объект <see cref="T:System.Configuration.ConfigurationElement"/>, для которого возвращается ключ. </param>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobPipelineConfigurationElement)element).Type;
        }

        #endregion

        #region Implementation of IJobPipelineConfiguration

        public IEnumerable<Type> JobBuildingStrategies
        {
            get
            {
                return from JobPipelineConfigurationElement element in this select element.Type;
            }
        }

        #endregion
    }
}
