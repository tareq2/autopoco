using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.FactoryActions
{
    public class ApplyTypeMemberConventions : IEngineConfigurationFactoryTypeAction
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventionProvider;

        public ApplyTypeMemberConventions(
            IEngineConfiguration configuration, 
            IEngineConventionProvider conventionProvider)
        {
            mConfiguration = configuration;
            mConventionProvider = conventionProvider;
        }

        public void Apply(IEngineConfigurationType type)
        {
            foreach (var member in type.GetRegisteredMembers())
            {
                // If it's already been set, then ignore it
                if (member.GetDatasources().FirstOrDefault() != null) { continue; }

                // Apply the appropriate conventions
                if (member.Member.IsField)
                {
                    mConventionProvider.Find<ITypeFieldConvention>()
                    .Select(t => (ITypeFieldConvention)Activator.CreateInstance(t))
                    .ToList()
                    .ForEach(x =>
                    {
                        TypeFieldConventionRequirements requirements = new TypeFieldConventionRequirements();
                        x.SpecifyRequirements(requirements);
                        if (requirements.IsValid((EngineTypeFieldMember)member.Member))
                        {
                            x.Apply(new TypeFieldConventionContext(mConfiguration, member));
                        }
                    });
                }
                if (member.Member.IsProperty)
                {
                    mConventionProvider.Find<ITypePropertyConvention>()
                    .Select(t => (ITypePropertyConvention)Activator.CreateInstance(t))
                    .ToList()
                    .ForEach(x =>
                    {
                        TypePropertyConventionRequirements requirements = new TypePropertyConventionRequirements();
                        x.SpecifyRequirements(requirements);
                        if (requirements.IsValid((EngineTypePropertyMember)member.Member))
                        {
                            x.Apply(new TypePropertyConventionContext(mConfiguration, member));
                        }
                    });
                }
            }           
        }
    }
}
