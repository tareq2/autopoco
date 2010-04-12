﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class DefaultStringSource : DatasourceBase<String>
    {
        public override string Next(IGenerationSession session)
        {
            return string.Empty;
        }
    }
}
