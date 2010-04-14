using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    /// <summary>
    /// This factory is the default for AutoPoco, and wraps up the base factory with some additional behaviour to scan through the registered types
    /// and bind together members from base classes and interfaces into derived types so behaviour defined for those base types/interfaces will propogate into those derived types
    /// </summary>
    public class DefaultEngineConfigurationFactory : EngineConfigurationFactory
    {
        public override IEngineConfiguration Create(Providers.IEngineConfigurationProvider configurationProvider, Providers.IEngineConventionProvider conventionProvider)
        {
            // Create an override configuration
            EngineConfiguration overrideConfiguration = new EngineConfiguration();

            // Create the default configuration
            IEngineConfiguration baseConfiguration = base.Create(configurationProvider, conventionProvider);
            
            // Apply base rules to derived types
            foreach (var sourceType in baseConfiguration.GetRegisteredTypes())
            {
                PostProcessType(overrideConfiguration, baseConfiguration, sourceType);
            }

            return overrideConfiguration;
        }

        protected virtual void PostProcessType(
            EngineConfiguration overrideConfiguration, 
            IEngineConfiguration baseConfiguration, 
            IEngineConfigurationType sourceType)
        {
            // Register that type with our new configuration
            overrideConfiguration.RegisterType(sourceType.RegisteredType);
            IEngineConfigurationType destinationType = overrideConfiguration.GetRegisteredType(sourceType.RegisteredType);

            // Create the dependency stack
            IEnumerable<IEngineConfigurationTypeMember> membersToApply = GetAllTypeHierarchyMembers(baseConfiguration, sourceType);

            foreach (var existingMemberConfig in membersToApply)
            {
                IEngineConfigurationTypeMember currentMemberConfig = destinationType.GetRegisteredMember(existingMemberConfig.Member);
                if (currentMemberConfig == null)
                {
                    destinationType.RegisterMember(existingMemberConfig.Member);
                    currentMemberConfig = destinationType.GetRegisteredMember(existingMemberConfig.Member);
                }
                currentMemberConfig.SetDatasources(existingMemberConfig.GetDatasources());
            }

        }

        protected virtual IEnumerable<IEngineConfigurationTypeMember> GetAllTypeHierarchyMembers(IEngineConfiguration baseConfiguration, IEngineConfigurationType sourceType)
        {
            Stack<IEngineConfigurationType> configurationStack = new Stack<IEngineConfigurationType>();
            Type currentType = sourceType.RegisteredType;
            IEngineConfigurationType currentTypeConfiguration = null;

            // Get all the base types into a stack, where the base-most type is at the top
            while (currentType != null)
            {
                currentTypeConfiguration = baseConfiguration.GetRegisteredType(currentType);
                if (currentTypeConfiguration != null) { configurationStack.Push(currentTypeConfiguration); }
                currentType = currentType.BaseType;
            }

            // Put all the implemented interfaces on top of that
            foreach (var interfaceType in sourceType.RegisteredType.GetInterfaces())
            {
                currentTypeConfiguration = baseConfiguration.GetRegisteredType(interfaceType);
                if (currentTypeConfiguration != null)
                {
                    configurationStack.Push(currentTypeConfiguration);
                }
            }

            var membersToApply = (from typeConfig in configurationStack
                                  from memberConfig in typeConfig.GetRegisteredMembers()
                                  select memberConfig).ToArray();

            return membersToApply;
        }
    }
}
