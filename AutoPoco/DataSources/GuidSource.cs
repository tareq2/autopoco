using System;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class GuidSource : DatasourceBase<Guid>
    {
        public override Guid Next(IGenerationSession session)
        {
            return Guid.NewGuid();
        }
    }
}