using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMemberBuilder : IEngineConfigurationTypeMemberBuilder, IEngineConfigurationTypeMemberProvider
    {
        protected EngineConfigurationTypeBuilder mParentConfiguration;
        protected EngineTypeMember mMember;
        protected DatasourceFactory mDatasource;

        public EngineConfigurationTypeMemberBuilder(EngineTypeMember member, EngineConfigurationTypeBuilder parentConfiguration)
        {
            mMember = member;
            mParentConfiguration = parentConfiguration;
        }

        #region IEngineConfigurationTypeMemberBuilder Members

        public IEngineConfigurationTypeBuilder Use(Type dataSource)
        {
            if (dataSource.GetInterface(typeof(IDatasource).FullName) == null) { throw new ArgumentException("dataSource does not implement IDatasource", "dataSource"); }
            mDatasource = new DatasourceFactory(dataSource);
            return mParentConfiguration;
        }

        public IEngineConfigurationTypeBuilder Use(Type dataSource, params object[] args)
        {
            if (dataSource.GetInterface(typeof(IDatasource).FullName) == null) { throw new ArgumentException("dataSource does not implement IDatasource", "dataSource"); }
            mDatasource = new DatasourceFactory(dataSource);
            mDatasource.SetParams(args);
            return mParentConfiguration;
        }

        public IEngineConfigurationTypeBuilder Default()
        {
            mDatasource = null;
            return mParentConfiguration;
        }

        #endregion

        public EngineTypeMember GetConfigurationMember()
        {
            return mMember;
        }

        public IEngineConfigurationDatasource GetDatasource()
        {
            return mDatasource;
        }
    }
}
