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

        private int ScoreRequirements(TypeMemberConventionRequirements requirements)
        {
            int score = 0;
            if (requirements.HasNameRule()) { score+=2; }
            if (requirements.HasTypeRule()) { score++; }
            return score;
        }

        public void Apply(IEngineConfigurationType type)
        {
            foreach (var member in type.GetRegisteredMembers())
            {
                // Apply the appropriate conventions
                if (member.Member.IsField)
                {
                    var convention = mConventionProvider.Find<ITypeFieldConvention>()
                    .Select(t =>
                    {
                        var details = new
                        {
                            Convention = (ITypeFieldConvention)Activator.CreateInstance(t),
                            Requirements = new TypeFieldConventionRequirements()
                        };
                        details.Convention.SpecifyRequirements(details.Requirements);
                        return details;
                    })
                    .Where(x => x.Requirements.IsValid((EngineTypeFieldMember)member.Member))
                    .OrderByDescending(x => ScoreRequirements(x.Requirements))
                    .FirstOrDefault();

                    if (convention != null)
                    {
                        convention.Convention.Apply(new TypeFieldConventionContext(mConfiguration, member));
                    }
                }
                if (member.Member.IsProperty)
                {
                    var convention = mConventionProvider.Find<ITypePropertyConvention>()
                         .Select(t =>
                         {
                             var details = new
                             {
                                 Convention = (ITypePropertyConvention)Activator.CreateInstance(t),
                                 Requirements = new TypePropertyConventionRequirements()
                             };
                             details.Convention.SpecifyRequirements(details.Requirements);
                             return details;
                         })
                         .Where(x => x.Requirements.IsValid((EngineTypePropertyMember)member.Member))
                         .OrderByDescending(x => ScoreRequirements(x.Requirements))
                         .FirstOrDefault();

                    if (convention != null)
                    {
                        convention.Convention.Apply(new TypePropertyConventionContext(mConfiguration, member));
                    }
                }
            }          
        }
    }
}
