using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationTypeBuilder
    {
        /// <summary>
        /// Adds a specific rule for a property on the poco we're building rules for
        /// </summary>
        IEngineConfigurationTypeMemberBuilder SetupProperty(string propertyName);

        /// <summary>
        /// Adds a specific rule for a field on the poco we're building rules for
        /// </summary>
        IEngineConfigurationTypeMemberBuilder SetupField(string fieldName);
    }
}
