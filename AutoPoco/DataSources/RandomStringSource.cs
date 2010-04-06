using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class RandomStringSource : DatasourceBase<String>
    {
        private int mMin;
        private int mMax;

        public RandomStringSource(int min, int max)
        {
            mMin = min;
            mMax = max;
        }

        public override string Next(IGenerationSession session)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            int length = random.Next(mMin, mMax + 1);
            for (int x = 0; x < length; x++)
            {
                int value = random.Next(65, 123);
                builder.Append((char)value);
            }
            return builder.ToString();
        }
    }
}
