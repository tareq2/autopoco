using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public class GenerationContextNode : IGenerationContextNode
    {
        private IGenerationContextNode mSite;
        private object mTargetObject;
        private EngineTypeMember mTargetMember;

        public GenerationContextNode(IGenerationContextNode parent, object targetObject, EngineTypeMember targetMember)
        {
            mSite = parent;
            mTargetObject = targetObject;
            mTargetMember = targetMember;       
        }

        public IGenerationContextNode Site
        {
            get { return mSite; }
        }

        public object TargetObject
        {
            get { return mTargetObject; }
        }

        public Configuration.EngineTypeMember TargetMember
        {
            get { return mTargetMember; }
        }
    }
}
