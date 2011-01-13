using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class ApplyTypeFactoryAction : TypeRegistrationAction
    {
        private IEngineConfigurationProvider mConfigurationProvider;

        public ApplyTypeFactoryAction(IEngineConfigurationProvider configurationProvider)
        {
            mConfigurationProvider = configurationProvider;
        }
        public override void Apply(IEngineConfigurationType type)
        {
            var typeProvider =
                mConfigurationProvider.GetConfigurationTypes().Where(x => x.GetConfigurationType() == type.RegisteredType)
                    .FirstOrDefault();
            
            if(typeProvider != null && typeProvider.GetFactory() != null)
            {
                type.SetFactory(typeProvider.GetFactory());
            }
        }
    }
}
