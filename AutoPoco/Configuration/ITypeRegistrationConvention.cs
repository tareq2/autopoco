using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    /// <summary>
    /// This convention is ran immediately after a type has been added to the core configuration object
    /// </summary>
    public interface ITypeRegistrationConvention : IConvention
    {
        /// <summary>
        /// Applies this type registration convention
        /// </summary>
        /// <param name="context"></param>
        void Apply(ITypeRegistrationConventionContext context);
    }
}
