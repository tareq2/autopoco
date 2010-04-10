using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.Actions
{
    public class ObjectMethodInvokeFromSourceAction : IObjectAction
    {
        private EngineTypeMethodMember mMember;
        private IEnumerable<IDatasource> mSources;

        public ObjectMethodInvokeFromSourceAction(EngineTypeMethodMember member, IEnumerable<IDatasource> sources)
        {
            mMember = member;
            mSources = sources.ToArray();
        }

        public void Enact(IGenerationSession session, object target)
        {
            List<Object> paramList = new List<object>();
            foreach (var source in mSources)
            {
                paramList.Add(source.Next(session));
            }
            mMember.MethodInfo.Invoke(target, paramList.ToArray());
        }
    }
}
