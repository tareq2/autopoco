using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationFactory : IEngineConfigurationFactory
    {
        public IEngineConfiguration Create(IEngineConfigurationProvider configurationProvider, IEngineConventionProvider conventionProvider)
        {
            EngineConfiguration configuration = new EngineConfiguration();

            FindAndRegisterAllTypes(configurationProvider, configuration);
            
            foreach (var type in configurationProvider.GetConfigurationTypes())
            {
                ApplyConventionsToType(conventionProvider, configuration, type);
                ApplyConfigurationToTypeMembers(configuration, type);
                ApplyConventionsToTypeMembers(conventionProvider, configuration, type);
            }
            return configuration;
        }

        public void ApplyConventionsToType(IEngineConventionProvider conventionProvider, IEngineConfiguration configuration, IEngineConfigurationTypeProvider type)
        {
            var configuredType = configuration.GetRegisteredType(type.GetConfigurationType());
            conventionProvider.Find<ITypeConvention>()
                .Select(t => (ITypeConvention)Activator.CreateInstance(t))
                .ToList()
                .ForEach(x =>
                {
                    x.Apply(new TypeConventionContext(configuredType));
                });
        }

        public void FindAndRegisterAllTypes(IEngineConfigurationProvider configurationProvider, EngineConfiguration configuration)
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

        public void ApplyConventionsToTypeMembers(IEngineConventionProvider conventionProvider, EngineConfiguration configuration, IEngineConfigurationTypeProvider type)
        {
            // Apply Member Conventions to all convention configured members
            var configuredType = configuration.GetRegisteredType(type.GetConfigurationType());
            foreach (var member in configuredType.GetRegisteredMembers())
            {
                // If it's already been set, then ignore it
                if (member.GetDatasources().FirstOrDefault() != null) { continue; }

                // Apply the appropriate conventions
                if (member.Member.IsField)
                {
                    conventionProvider.Find<ITypeFieldConvention>()
                    .Select(t => (ITypeFieldConvention)Activator.CreateInstance(t))
                    .ToList()
                    .ForEach(x =>
                    {
                        x.Apply(new TypeFieldConventionContext(configuration, member));
                    });
                }
                if (member.Member.IsProperty)
                {
                    conventionProvider.Find<ITypePropertyConvention>()
                    .Select(t => (ITypePropertyConvention)Activator.CreateInstance(t))
                    .ToList()
                    .ForEach(x =>
                    {
                        x.Apply(new TypePropertyConventionContext(configuration, member));
                    });
                }
            }
        }

        public void ApplyConfigurationToTypeMembers(EngineConfiguration configuration, IEngineConfigurationTypeProvider type)
        {
            var configuredType = configuration.GetRegisteredType(type.GetConfigurationType());
            foreach (var member in type.GetConfigurationMembers())
            {
                EngineTypeMember typeMember = member.GetConfigurationMember();

                // Register the member if necessary
                var configuredMember = configuredType.GetRegisteredMember(typeMember);
                if (configuredMember == null)
                {
                    configuredType.RegisterMember(typeMember);
                    configuredMember = configuredType.GetRegisteredMember(typeMember);
                }

                // Set the action on that member if a datasource has been set explicitly for this type
                var datasources = member.GetDatasources();
                if (datasources.Count() > 0)
                {
                    configuredMember.SetDatasources(datasources);
                }
            }
        }

    }
}
