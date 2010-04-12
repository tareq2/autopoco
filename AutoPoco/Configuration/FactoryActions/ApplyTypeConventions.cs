using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.FactoryActions
{
    public class ApplyTypeConventions : IEngineConfigurationFactoryTypeAction
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventionProvider;

        public ApplyTypeConventions(IEngineConfiguration configuration, IEngineConventionProvider conventionProvider)
        {
            mConfiguration = configuration;
            mConventionProvider = conventionProvider;
        }

        public void Apply(IEngineConfigurationType type)
        {
            mConventionProvider.Find<ITypeConvention>()
                .Select(t => (ITypeConvention)Activator.CreateInstance(t))
                .ToList()
                .ForEach(x =>
                {
                    x.Apply(new TypeConventionContext(type));
                });
        }
    }
}
