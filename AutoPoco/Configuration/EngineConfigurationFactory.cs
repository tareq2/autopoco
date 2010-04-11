using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;
using AutoPoco.Configuration.FactoryActions;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationFactory : IEngineConfigurationFactory
    {
        public IEngineConfiguration Create(IEngineConfigurationProvider configurationProvider, IEngineConventionProvider conventionProvider)
        {
            EngineConfiguration configuration = new EngineConfiguration();
            List<IEngineConfigurationFactoryTypeAction> actions = new List<IEngineConfigurationFactoryTypeAction>();
            
            // Create the actions
            actions.Add(new ApplyTypeConventions(
                configuration, conventionProvider));
            actions.Add(new ApplyTypeMemberConfiguration(
                configurationProvider, configuration, conventionProvider));
            actions.Add(new ApplyTypeMemberConventions(
               configuration, conventionProvider));

            // Scan for the types
            FindAndRegisterAllTypes(configurationProvider, configuration);

            foreach (var type in configuration.GetRegisteredTypes())
            {
                // Apply all actions to that type             
                actions.ForEach(a => a.Apply(type));
            }
            return configuration;
        }

        protected virtual void FindAndRegisterAllTypes(IEngineConfigurationProvider configurationProvider, EngineConfiguration configuration)
        {
            // Perform all type registration
            foreach (var type in configurationProvider.GetConfigurationTypes())
            {
                Type configType = type.GetConfigurationType();

                // Register the type if necessary
                var configuredType = configuration.GetRegisteredType(configType);
                if (configuredType == null)
                {
                    configuration.RegisterType(configType);
                    configuredType = configuration.GetRegisteredType(configType);
                }
            }
        }
    }
}
