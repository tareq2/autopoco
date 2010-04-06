using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public interface ITypePropertyConvention : IConvention
    {
        /// <summary>
        /// Apply the convention to the registered property
        /// </summary>
        void Apply(ITypePropertyConventionContext context);
    }
}
