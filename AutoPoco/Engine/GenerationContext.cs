using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public class GenerationContext : IGenerationContext
    {
        private IGenerationSession mGenerationSession;
        private IGenerationContextNode mDatasourceContextNode;

        public GenerationContext(IGenerationSession generationSession, IGenerationContextNode datasourceContextNode)
        {
            this.mGenerationSession = generationSession;
            this.mDatasourceContextNode = datasourceContextNode;
        }
        public IGenerationSession Session
        {
            get { return mGenerationSession; }
        }

        public IGenerationContextNode Site
        {
            get { return mDatasourceContextNode; }
        }
    }
}
