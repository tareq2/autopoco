using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public interface ITypeConventionContext
    {
        /// <summary>
        /// Gets the type being registered
        /// </summary>
        Type Target
        {
            get;
        }

        /// <summary>
        /// Registers a field for auto-population
        /// </summary>
        /// <param name="fieldName"></param>
        void RegisterField(FieldInfo field);

        /// <summary>
        /// Registers a property for auto-population
        /// </summary>
        /// <param name="propertyName"></param>
        void RegisterProperty(PropertyInfo property);

        // TODO: Register Methods for invocation
    }
}
