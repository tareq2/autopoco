using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    /// <summary>
    /// Defines a registered member as part of a type
    /// </summary>
    public interface IEngineConfigurationTypeMember
    {
        /// <summary>
        /// Gets the member this configuration is a part of
        /// </summary>
        EngineTypeMember Member
        {
            get;
        }

        /// <summary>
        /// Sets the action being performed to this member on type instantation
        /// </summary>
        /// <param name="action"></param>
        void SetSource(IEngineConfigurationDatasource action);

        /// <summary>
        /// Gets the action being performed to this member on type instantiation
        /// </summary>
        /// <returns></returns>
        IEngineConfigurationDatasource GetSource();

    }
}
