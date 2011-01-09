﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Conventions
{
    // A Klooge, but it's better than what I had before, I'll work
    public class DefaultTypeRegistrationConvention : ITypeRegistrationConvention
    {
        public void Apply(ITypeRegistrationConventionContext context)
        {
            ApplyTypeConventions(context);
            RegisterTypeMembersFromConfiguration(context);
            ApplyTypeMemberConventions(context);
            ApplyTypeMemberConfiguration(context);
            ScanAndRegisterBaseTypeMembers(context);           
        }

        public virtual void ScanAndRegisterBaseTypeMembers(ITypeRegistrationConventionContext context)
        {
            // Create the dependency stack
            IEnumerable<IEngineConfigurationTypeMember> membersToApply = GetAllTypeHierarchyMembers(context.Configuration, context.Target);

            foreach (var existingMemberConfig in membersToApply)
            {
                IEngineConfigurationTypeMember currentMemberConfig = context.Target.GetRegisteredMember(existingMemberConfig.Member);
                if (currentMemberConfig == null)
                {
                    context.Target.RegisterMember(existingMemberConfig.Member);
                    currentMemberConfig = context.Target.GetRegisteredMember(existingMemberConfig.Member);

                    // Note: We only override these if it wasn't configured already
                    currentMemberConfig.SetDatasources(existingMemberConfig.GetDatasources());
                }             
            }
        }

        public virtual void ApplyTypeConventions(ITypeRegistrationConventionContext context)
        {
            context.ConventionProvider.Find<ITypeConvention>()
            .Select(t => (ITypeConvention)Activator.CreateInstance(t))
            .ToList()
            .ForEach(x =>
            {
                x.Apply(new TypeConventionContext(context.Target));
            });
        }

        public virtual void RegisterTypeMembersFromConfiguration(ITypeRegistrationConventionContext context)
        {
            var typeProviders = context.ConfigurationProvider.GetConfigurationTypes().Where(x => x.GetConfigurationType() == context.Target.RegisteredType);

            foreach (var typeProvider in typeProviders)
            {
                foreach (var member in typeProvider.GetConfigurationMembers())
                {
                    EngineTypeMember typeMember = member.GetConfigurationMember();

                    if (context.Target.GetRegisteredMember(typeMember) == null)
                    {
                        context.Target.RegisterMember(typeMember);
                    }
                }
            }
        }

        public virtual void ApplyTypeMemberConventions(ITypeRegistrationConventionContext context)
        {
            context.ConventionProvider.ApplyTypeConventions(context.Configuration, context.Target);
        }

        public virtual void ApplyTypeMemberConfiguration(ITypeRegistrationConventionContext context)
        {
            var typeProviders = context.ConfigurationProvider.GetConfigurationTypes()
                .Where(x => x.GetConfigurationType() == context.Target.RegisteredType);

            foreach (var typeProvider in typeProviders)
            {
                foreach (var memberProvider in typeProvider.GetConfigurationMembers())
                {
                    EngineTypeMember typeMember = memberProvider.GetConfigurationMember();

                    // Get the member
                    var configuredMember = context.Target.GetRegisteredMember(typeMember);

                    // Set the action on that member if a datasource has been set explicitly for this member
                    var datasources = memberProvider.GetDatasources();
                    if (datasources.Count() > 0)
                    {
                        configuredMember.SetDatasources(datasources);
                    }
                }
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