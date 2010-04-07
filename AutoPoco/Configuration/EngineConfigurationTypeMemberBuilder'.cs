using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMemberBuilder<TPoco, TMember> : EngineConfigurationTypeMemberBuilder, IEngineConfigurationTypeMemberBuilder<TPoco, TMember>
    {
        private IEngineConfigurationTypeBuilder<TPoco> mParentConfiguration;

        public EngineConfigurationTypeMemberBuilder(EngineTypeMember member, EngineConfigurationTypeBuilder<TPoco> parentConfiguration)
            : base(member, parentConfiguration)
        {
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
    }
}
