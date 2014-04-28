﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomStringSource.cs" company="AutoPoco">
//   Microsoft Public License (Ms-PL)
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoPoco.DataSources
{
    using System.Text;

    using AutoPoco.Engine;
    using AutoPoco.Util;

    /// <summary>
    /// The random string source.
    /// </summary>
    public class RandomStringSource : DatasourceBase<string>
    {
        #region Fields

        /// <summary>
        /// The max.
        /// </summary>
        private readonly int max;

        /// <summary>
        /// The min.
        /// </summary>
        private readonly int min;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomStringSource"/> class.
        /// </summary>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        public RandomStringSource(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The next.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string Next(IGenerationContext context)
        {
            var builder = new StringBuilder();
            int length = RandomNumberGenerator.Current.Next(this.min, this.max + 1);

            for (int x = 0; x < length; x++)
            {
                int value = RandomNumberGenerator.Current.Next(65, 123);
                builder.Append((char)value);
            }

            return builder.ToString();
        }

        #endregion
    }
}