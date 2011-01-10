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
          
            FindAndRegisterAllBaseTypes(configurationProvider, configuration, conventionProvider);
            RunRegistrationConventionsAgainstTypes(configurationProvider, conventionProvider, configuration);
                       
            return configuration;
        }

        private static void RunRegistrationConventionsAgainstTypes(IEngineConfigurationProvider configurationProvider, IEngineConventionProvider conventionProvider, EngineConfiguration configuration)
        {
            foreach (var type in configuration.GetRegisteredTypes())
            {
                var registrationConvention = conventionProvider.Find<ITypeRegistrationConvention>().FirstOrDefault();

                if (registrationConvention != null)
                {
                    ITypeRegistrationConvention instance = (ITypeRegistrationConvention)Activator.CreateInstance(registrationConvention);
                    instance.Apply(new TypeRegistrationConventionContext(configuration, configurationProvider, conventionProvider, type));
                }
            }
        }

        protected virtual void FindAndRegisterAllBaseTypes(IEngineConfigurationProvider configurationProvider, EngineConfiguration configuration,  IEngineConventionProvider conventionProvider)
        {
            foreach (var type in configurationProvider.GetConfigurationTypes())
            {
                TryRegisterType(configuration, type.GetConfigurationType());
            }
        }

        private static void TryRegisterType(EngineConfiguration configuration, Type configType)
        {
            var configuredType = configuration.GetRegisteredType(configType);
            if (configuredType == null)
            {
                configuration.RegisterType(configType);
                configuredType = configuration.GetRegisteredType(configType);
            }

            foreach (var interfaceType in configType.GetInterfaces())
            {
                TryRegisterType(configuration, interfaceType);
            }

            Type baseType = configType.BaseType;
            if(baseType != null) { TryRegisterType(configuration, baseType);}
        }
    }
}
