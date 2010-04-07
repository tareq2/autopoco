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
    public class EngineConfigurationTypeBuilder : IEngineConfigurationTypeProvider, IEngineConfigurationTypeBuilder
    {
        protected List<IEngineConfigurationTypeMemberProvider> mMembers = new List<IEngineConfigurationTypeMemberProvider>();
        private Type mType;

        public Type GetConfigurationType()
        {
            return mType;
        }

        public IEnumerable<IEngineConfigurationTypeMemberProvider> GetConfigurationMembers()
        {
            return mMembers;
        }

        public EngineConfigurationTypeBuilder(Type type)
        {
            mType = type;
        }

        public IEngineConfigurationTypeMemberBuilder SetupProperty(string propertyName)
        {
            throw new NotImplementedException();
        }

        public IEngineConfigurationTypeMemberBuilder SetupField(string fieldName)
        {
            throw new NotImplementedException();
        }
    }
}
