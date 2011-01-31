﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Testing
{
    public class BlankDataSource : DatasourceBase<int>
    {
        public override int Next(IGenerationContext context)
        {
            return 0;
        }
    }
}
