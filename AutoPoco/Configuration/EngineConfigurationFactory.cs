using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationFactory : IEngineConfigurationFactory
    {
        public virtual IEngineConfiguration Create(IEngineConfigurationProvider configurationProvider, IEngineConventionProvider conventionProvider)
        {
            EngineConfiguration configuration = new EngineConfiguration();
                    
            // Scan for the types
            FindAndRegisterAllTypes(configurationProvider, configuration, conventionProvider);
            
            foreach (var type in configuration.GetRegisteredTypes())
            {
                // Run registration conventions against it
                var registrationConvention = conventionProvider.Find<ITypeRegistrationConvention>().FirstOrDefault();

                if (registrationConvention != null)
                {
                    ITypeRegistrationConvention instance = (ITypeRegistrationConvention)Activator.CreateInstance(registrationConvention);
                    instance.Apply(new TypeRegistrationConventionContext(configuration, configurationProvider, conventionProvider, type));
                }
            }
                       
            // Job done
            return configuration;
        }

        protected virtual void FindAndRegisterAllTypes(IEngineConfigurationProvider configurationProvider, EngineConfiguration configuration,  IEngineConventionProvider conventionProvider)
        {
            // Perform all type registration
            foreach (var type in configurationProvider.GetConfigurationTypes())
            {
                TryRegisterType(configuration, type.GetConfigurationType());
            }
        }

        private static void TryRegisterType(EngineConfiguration configuration, Type configType)
        {
            // Register the type if necessary
            var configuredType = configuration.GetRegisteredType(configType);
            if (configuredType == null)
            {
                configuration.RegisterType(configType);
                configuredType = configuration.GetRegisteredType(configType);
            }

            Type baseType = configType.BaseType;
            if(baseType != null) { TryRegisterType(configuration, baseType);}
        }
    }
}
