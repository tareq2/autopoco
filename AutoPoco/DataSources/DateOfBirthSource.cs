using System;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class DateOfBirthSource : DatasourceBase<DateTime>
    {
        private readonly Random _random;
        private readonly int _yearsMax;
        private readonly int _yearsMin;

        public DateOfBirthSource()
            :this(16, 59)
        {}

        public DateOfBirthSource(int min, int max)
        {
            _random = new Random();
            _yearsMax = max;
            _yearsMin = min;
        }

        public override DateTime Next(IGenerationSession session)
        {
            return DateTime.Now.AddYears(-_random.Next(_yearsMin, _yearsMax));
        }
    }
}