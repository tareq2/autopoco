using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AutoPoco.Util;
using AutoPoco.Configuration.Providers;
using AutoPoco.Engine;
using System.Reflection;

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
            MemberInfo info = mType.GetProperty(propertyName);
            if (info == null) { throw new ArgumentException("Property does not exist", propertyName); }
            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            return memberBuilder;
        }

        public IEngineConfigurationTypeMemberBuilder SetupField(string fieldName)
        {
            FieldInfo info = mType.GetField(fieldName);
            if (info == null) { throw new ArgumentException("Field does not exist", fieldName); }
            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            return memberBuilder;
        }
    }
}
