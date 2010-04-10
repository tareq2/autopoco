using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationBuilder : IEngineConfigurationBuilder, IEngineConfigurationProvider
    {
        private EngineConventionConfiguration mConventions = new EngineConventionConfiguration();
        private List<IEngineConfigurationTypeProvider> mTypes = new List<IEngineConfigurationTypeProvider>();

        public IEngineConfigurationTypeBuilder<T> Include<T>() where T : new()
        {
            // Create the configuration
            var configuration = new EngineConfigurationTypeBuilder<T>();

            // Store it locally
            mTypes.Add(configuration);

            //And return the public interface
            return (IEngineConfigurationTypeBuilder<T>)configuration;
        }

        public IEngineConfigurationTypeBuilder Include(Type t)
        {
            // Create the configuration
            var configuration = new EngineConfigurationTypeBuilder(t);

            // Store it locally
            mTypes.Add(configuration);

            //And return the public interface
            return configuration;
        }        
       
        public void Conventions(Action<IEngineConventionConfiguration> config)
        {
            config.Invoke(mConventions);
        }

        public IEnumerable<IEngineConfigurationTypeProvider> GetConfigurationTypes()
        {
            return mTypes;
        }

        public IEngineConfiguration Build()
        {
            EngineConfiguration configuration = new EngineConfiguration();

            // Perform all type registration
            foreach (var type in this.GetConfigurationTypes())
            {
                Type configType = type.GetConfigurationType();

                // Register the type if necessary
                var configuredType = configuration.GetRegisteredType(configType);
                if (configuredType == null)
                {
                    configuration.RegisterType(configType);
                    configuredType = configuration.GetRegisteredType(configType);

                    // Apply Type Conventions
                    mConventions.Find<ITypeConvention>()
                        .Select(t => (ITypeConvention)Activator.CreateInstance(t))
                        .ToList()
                        .ForEach(x =>
                        {
                            x.Apply(new TypeConventionContext(configuredType));
                        });
                }
            }

            // Go through all of the explicitly-registered types and do their members
            foreach (var type in this.GetConfigurationTypes())
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

                // Apply Member Conventions to all convention configured members
                foreach (var member in configuredType.GetRegisteredMembers())
                {
                    // If it's already been set, then ignore it
                    if (member.GetDatasources().FirstOrDefault() != null) { continue; }

                    // Apply the appropriate conventions
                    if (member.Member.IsField)
                    {
                        mConventions.Find<ITypeFieldConvention>()
                        .Select(t => (ITypeFieldConvention)Activator.CreateInstance(t))
                        .ToList()
                        .ForEach(x =>
                        {
                            x.Apply(new TypeFieldConventionContext(configuration, member));
                        });
                    }
                    if (member.Member.IsProperty)
                    {
                        mConventions.Find<ITypePropertyConvention>()
                        .Select(t => (ITypePropertyConvention)Activator.CreateInstance(t))
                        .ToList()
                        .ForEach(x =>
                        {
                            x.Apply(new TypePropertyConventionContext(configuration, member));
                        });
                    }
                }

            }
            return configuration;
        }

        public void RegisterTypeProvider(IEngineConfigurationTypeProvider provider)
        {
            mTypes.Add(provider);
        }
    }
}
