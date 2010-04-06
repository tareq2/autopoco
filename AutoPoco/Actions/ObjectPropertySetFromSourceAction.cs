using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public class ObjectPropertySetFromSourceAction : IObjectAction
    {
        private EngineTypePropertyMember mMember;
        private IDatasource mDatasource;

        public ObjectPropertySetFromSourceAction(EngineTypePropertyMember member, IDatasource source)
        {
            mMember = member;
            mDatasource = source;
        }

        public void Enact(IGenerationSession session, object target)
        {
            mMember.PropertyInfo.SetValue(target, this.mDatasource.Next(session), null);
        }
    }
}
