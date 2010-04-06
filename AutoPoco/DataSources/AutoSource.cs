using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class AutoSource<T> : DatasourceBase<T>
    {
        public override T Next(IGenerationSession session)
        {
            return session.With<T>().Get();
        }
    }
}
