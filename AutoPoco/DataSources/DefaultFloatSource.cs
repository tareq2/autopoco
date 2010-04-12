using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class DefaultFloatSource : DatasourceBase<float>
    {
        public override float Next(IGenerationSession session)
        {
            return 0;
        }
    }

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

    public class GuidSource : DatasourceBase<Guid>
    {
        #region Overrides of DatasourceBase<Guid>

        public override Guid Next(IGenerationSession session)
        {
            return Guid.NewGuid();
        }

        #endregion
    }

    public class ColorSource : DatasourceBase<Color>
    {
        private readonly int _max;
        private readonly Random _random;
        private readonly int _min;

        public ColorSource()
        {
            _random = new Random();
            _min = 0;
            _max = 255;
        }

        #region Overrides of DatasourceBase<Color>

        public override Color Next(IGenerationSession session)
        {
            return Color.FromArgb(
                    _random.Next(_min, _max), 
                    _random.Next(_min, _max), 
                    _random.Next(_min, _max)
                );
        }

        #endregion
    }

}
