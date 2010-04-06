using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AutoPoco.Util;
using AutoPoco.Configuration.Providers;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeBuilder<TPoco> : IEngineConfigurationTypeBuilder<TPoco>, IEngineConfigurationTypeProvider
    {
        List<IEngineConfigurationTypeMemberProvider> mMembers = new List<IEngineConfigurationTypeMemberProvider>();

        public IEngineConfigurationTypeMemberBuilder<TPoco, TMember> Setup<TMember>(Expression<Func<TPoco, TMember>> expression)
        {           
            // Get the member this set up is for
            EngineTypeMember member = ReflectionHelper.GetMember(expression);

            // Create the configuration builder
            var configuration = new EngineConfigurationTypeMemberBuilder<TPoco, TMember>(member, this);

            // Store it in the local list
            mMembers.Add(configuration);

            // And return it
            return (IEngineConfigurationTypeMemberBuilder<TPoco, TMember>)configuration;
        }

        public Type GetConfigurationType()
        {
            return typeof(TPoco);
        }

        public IEnumerable<IEngineConfigurationTypeMemberProvider> GetConfigurationMembers()
        {
            return mMembers;
        }
    }
}
