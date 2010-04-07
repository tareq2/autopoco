using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Util;
using System.Linq.Expressions;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeBuilder<TPoco> : EngineConfigurationTypeBuilder, IEngineConfigurationTypeBuilder<TPoco>
    {
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

        public EngineConfigurationTypeBuilder() : base(typeof(TPoco)) { }
    }
}
