using System;
using System.Drawing;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
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