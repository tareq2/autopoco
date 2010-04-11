﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    class EngineConfigurationFactoryApplyTypeMemberConfigurationTypeAction : IEngineConfigurationFactoryTypeAction
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventionProvider;
        private IEngineConfigurationProvider mConfigurationProvider;

        public EngineConfigurationFactoryApplyTypeMemberConfigurationTypeAction(
            IEngineConfigurationProvider configurationProvider,
            IEngineConfiguration configuration, 
            IEngineConventionProvider conventionProvider)
        {
            mConfigurationProvider = configurationProvider;
            mConfiguration = configuration;
            mConventionProvider = conventionProvider;
        }

        public void Apply(IEngineConfigurationType type)
        {
            IEngineConfigurationTypeProvider typeProvider = mConfigurationProvider.GetConfigurationTypes().Where(x => x.GetConfigurationType() == type.RegisteredType).SingleOrDefault();

            foreach (var member in typeProvider.GetConfigurationMembers())
            {
                EngineTypeMember typeMember = member.GetConfigurationMember();

                // Register the member if necessary
                var configuredMember = type.GetRegisteredMember(typeMember);
                if (configuredMember == null)
                {
                    type.RegisterMember(typeMember);
                    configuredMember = type.GetRegisteredMember(typeMember);
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
