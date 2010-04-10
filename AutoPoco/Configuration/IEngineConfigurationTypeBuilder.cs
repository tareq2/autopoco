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

        /// <summary>
        /// Adds a rule for a method
        /// </summary>
        /// <param name="methodName">The method to be invoked on creation</param>
        /// <param name="args">The parameters to pass into the method. If the arg is a 'Type' that derives from IDatasource, this will be invoked and the created object shall be used as the paremeter</param>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder SetupMethod(string methodName, params Object[] args);
    }
}
