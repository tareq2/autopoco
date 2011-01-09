using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConventionConfiguration : IEngineConventionConfiguration, IEngineConventionProvider
    {
        private List<Type> mConventions = new List<Type>();

        public void Register(Type conventionType)
        {
            mConventions.Add(conventionType);
        }

        public void Register<T>() where T : IConvention
        {
            Register(typeof(T));
        }

        public void UseDefaultConventions()
        {
            ScanAssemblyWithType<EngineConventionConfiguration>();
        }

        public void ScanAssemblyWithType<T>()
        {
            ScanAssembly(typeof(T).Assembly);
        }

        public void ScanAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes()
               .Where(x => typeof(IConvention).IsAssignableFrom(x)))
            {
                Register(type);
            }
        }

        public IEnumerable<Type> Find<T>() where T : IConvention
        {
            return mConventions.Where(x => 
                x.IsClass 
                && typeof(T).IsAssignableFrom(x)
                && !x.IsAbstract);
        }

        public void ApplyTypeConventions(IEngineConfiguration configuration, IEngineConfigurationType type)
        {
            foreach (var member in type.GetRegisteredMembers())
            {
                // Apply the appropriate conventions
                if (member.Member.IsField)
                {
                    var convention = this.Find<ITypeFieldConvention>()
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
                        convention.Convention.Apply(new TypeFieldConventionContext(configuration, member));
                    }
                }
                if (member.Member.IsProperty)
                {
                    var convention = this.Find<ITypePropertyConvention>()
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
                        convention.Convention.Apply(new TypePropertyConventionContext(configuration, member));
                    }
                }
            }
        }

        private int ScoreRequirements(TypeMemberConventionRequirements requirements)
        {
            int score = 0;
            if (requirements.HasNameRule()) { score += 2; }
            if (requirements.HasTypeRule()) { score++; }
            return score;
        }

    }
}
