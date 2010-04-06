using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using System.Reflection;
using AutoPoco.Engine;

namespace AutoPoco.Actions
{
    public class ObjectFieldSetFromSourceAction : IObjectAction
    {
        private EngineTypeFieldMember mMember;
        private IDatasource mDatasource;

        public ObjectFieldSetFromSourceAction(EngineTypeFieldMember member, IDatasource source) 
        {
            mMember = member;
            mDatasource = source;
        }

        public void Enact(IGenerationSession session, object target)
        {
            mMember.FieldInfo.SetValue(target, mDatasource.Next(session));
        }
    }
}
