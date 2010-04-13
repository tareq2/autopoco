using System;
using System.Globalization;
using System.Linq;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class CountrySource : DatasourceBase<string>
    {
        private readonly Random _random;
        private readonly CultureInfo[] _cultures;

        public CountrySource()
        {
            _random = new Random();
            _cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
        }

        #region Overrides of DatasourceBase<string>

        public override string Next(IGenerationSession session)
        {
            string country = string.Empty;
            
            // skip the invariant culture (not a country)
            do
            {
                var index = _random.Next(1, _cultures.Count() - 1);
                country = _cultures[index].EnglishName;
            
                // some are combination of countries, let's skip them
            } while (country.Contains(","));

            // find the country
            int startIndex = country.IndexOf("(") + 1;
            country = country.Substring(startIndex).Replace(")", string.Empty);

            return country;
        }

        #endregion
    }
}