using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMember : IEngineConfigurationTypeMember
    {
        private IEngineConfigurationDatasource mDataSource;
        private EngineTypeMember mMember;

        public EngineConfigurationTypeMember(EngineTypeMember member)
        {
            mMember = member;
        }

        public EngineTypeMember Member
        {
            get { return mMember; }
        }

        public void SetSource(IEngineConfigurationDatasource action)
        {
            mDataSource = action;
        }

        public IEngineConfigurationDatasource GetSource()
        {
            return mDataSource;
        }
    }
}
