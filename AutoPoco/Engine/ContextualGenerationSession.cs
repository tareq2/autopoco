using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public class ContextualGenerationSession : IGenerationSession
    {
        private IGenerationSession mInnerSession;

        public ContextualGenerationSession(IGenerationSession innerSession)
        {
            mInnerSession = innerSession;
        }

        public IObjectGenerator<TPoco> Single<TPoco>()
        {
            throw new NotImplementedException();
        }

        public ICollectionContext<TPoco, IList<TPoco>> List<TPoco>(int count)
        {
            throw new NotImplementedException();
        }

        public TPoco Next<TPoco>()
        {
            throw new NotImplementedException();
        }

        public TPoco Next<TPoco>(Action<IObjectGenerator<TPoco>> cfg)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPoco> Collection<TPoco>(int count)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TPoco> Collection<TPoco>(int count, Action<ICollectionContext<TPoco, IList<TPoco>>> cfg)
        {
            throw new NotImplementedException();
        }
    }
}
