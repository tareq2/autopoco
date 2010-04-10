using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AutoPoco.Util;
using AutoPoco.Configuration.Providers;
using AutoPoco.Engine;
using System.Reflection;
using AutoPoco.DataSources;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeBuilder : IEngineConfigurationTypeProvider, IEngineConfigurationTypeBuilder
    {
        private List<IEngineConfigurationTypeMemberProvider> mMembers = new List<IEngineConfigurationTypeMemberProvider>();
        private Type mType;

        public EngineConfigurationTypeBuilder(Type type)
        {
            mType = type;
        }

        Type IEngineConfigurationTypeProvider.GetConfigurationType()
        {
            return mType;
        }

        IEnumerable<IEngineConfigurationTypeMemberProvider> IEngineConfigurationTypeProvider.GetConfigurationMembers()
        {
            return mMembers;
        }

        IEngineConfigurationTypeMemberBuilder IEngineConfigurationTypeBuilder.SetupProperty(string propertyName)
        {
            MemberInfo info = mType.GetProperty(propertyName);
            if (info == null) { throw new ArgumentException("Property does not exist", propertyName); }
            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            return memberBuilder;
        }

        IEngineConfigurationTypeMemberBuilder IEngineConfigurationTypeBuilder.SetupField(string fieldName)
        {
            FieldInfo info = mType.GetField(fieldName);
            if (info == null) { throw new ArgumentException("Field does not exist", fieldName); }
            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            return memberBuilder;
        }

        public IEngineConfigurationTypeBuilder SetupMethod(string methodName, params object[] args)
        {
            MethodInfo info = mType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(x => 
                x.Name == methodName 
                && x.GetParameters().Length == args.Length)
                .FirstOrDefault();

            if (info == null) { throw new ArgumentException("Method does not exist", methodName); }

            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);

            // Map the arguments to data sources
            List<DatasourceFactory> datasourceFactories = new List<DatasourceFactory>();
            foreach (var arg in args)
            {
                // If it's a datasource, then we need to use that data source
                if (arg.GetType().IsAssignableFrom(typeof(DatasourceFactory)))
                {
                    datasourceFactories.Add((DatasourceFactory)arg);
                }
                else
                {
                    // Literal
                    DatasourceFactory factory = new DatasourceFactory(typeof(ValueSource));
                    factory.SetParams(arg);
                    datasourceFactories.Add(factory);
                }
                memberBuilder.SetDatasources(datasourceFactories.ToArray());
            }

            return this;
        }

        protected void RegisterTypeMemberProvider(IEngineConfigurationTypeMemberProvider memberProvider)
        {
            this.mMembers.Add(memberProvider);
        }
    }
}
