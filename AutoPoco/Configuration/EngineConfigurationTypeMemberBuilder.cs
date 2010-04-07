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
            throw new NotImplementedException();
        }

        public IEngineConfigurationTypeBuilder Use(Type dataSource, params object[] args)
        {
            throw new NotImplementedException();
        }

        public IEngineConfigurationTypeBuilder Default()
        {
            throw new NotImplementedException();
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
