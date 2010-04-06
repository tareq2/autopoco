using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class DefaultIntegerSource : DatasourceBase<int>
    {
        public override int Next(IGenerationSession session)
        {
            return 0;
        }
    }
}
