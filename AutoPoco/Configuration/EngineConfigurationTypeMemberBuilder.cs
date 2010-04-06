using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMemberBuilder<TPoco, TMember> : IEngineConfigurationTypeMemberBuilder<TPoco, TMember>, IEngineConfigurationTypeMemberProvider
    {
        private EngineConfigurationTypeBuilder<TPoco> mParentConfiguration;
        private EngineTypeMember mMember;
        private DatasourceFactory mDatasource;

        public EngineConfigurationTypeMemberBuilder(EngineTypeMember member, EngineConfigurationTypeBuilder<TPoco> parentConfiguration)
        {
            mMember = member;
            mParentConfiguration = parentConfiguration;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Use<TSource>() where TSource : IDatasource<TMember>
        {
            mDatasource = new DatasourceFactory(typeof(TSource));
            return mParentConfiguration;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Use<TSource>(params Object[] args) where TSource : IDatasource<TMember>
        {
            mDatasource = new DatasourceFactory(typeof(TSource));
            mDatasource.SetParams(args);
            return mParentConfiguration;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Default()
        {
            mDatasource = null;
            return mParentConfiguration;
        }

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
