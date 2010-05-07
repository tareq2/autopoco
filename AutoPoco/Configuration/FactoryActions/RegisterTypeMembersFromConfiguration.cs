using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.FactoryActions
{
    public class RegisterTypeMembersFromConfiguration: IEngineConfigurationFactoryTypeAction
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventionProvider;
        private IEngineConfigurationProvider mConfigurationProvider;

        public RegisterTypeMembersFromConfiguration(
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
            var typeProviders = mConfigurationProvider.GetConfigurationTypes().Where(x => x.GetConfigurationType() == type.RegisteredType);

            foreach (var typeProvider in typeProviders)
            {
                foreach (var member in typeProvider.GetConfigurationMembers())
                {
                    EngineTypeMember typeMember = member.GetConfigurationMember();

                    // Register the member if necessary
                    if (type.GetRegisteredMember(typeMember) == null)
                    {
                        type.RegisterMember(typeMember);
                    }
                }
            }
        }

    }
}
