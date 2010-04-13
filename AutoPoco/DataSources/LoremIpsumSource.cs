using System.Text;
using AutoPoco.Engine;
using AutoPoco.Properties;

namespace AutoPoco.DataSources
{
    public class LoremIpsumSource : DatasourceBase<string>
    {
        private readonly int _times;

        public LoremIpsumSource()
            : this(1)
        {}

        public LoremIpsumSource(int times)
        {
            _times = times;
        }

        #region Overrides of DatasourceBase<string>

        public override string Next(IGenerationSession session)
        {
            var builder = new StringBuilder(Resources.LoremIpsum);

            for (int i = 1; i < _times; i++)
            {
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine(Resources.LoremIpsum);
            }

            return builder.ToString();
        }

        #endregion
    }
}