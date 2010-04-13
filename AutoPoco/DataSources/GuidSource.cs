using System;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class GuidSource : DatasourceBase<Guid>
    {
        #region Overrides of DatasourceBase<Guid>

        public override Guid Next(IGenerationSession session)
        {
            return Guid.NewGuid();
        }

        #endregion
    }
}