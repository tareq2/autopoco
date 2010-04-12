using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class ValueSource : IDatasource
    {
        private Object mValue;

        public ValueSource(Object value)
        {
            mValue = value;
        }

        public object Next(IGenerationSession session)
        {
            return mValue;
        }
    }
}
