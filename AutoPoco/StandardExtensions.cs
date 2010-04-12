using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;

namespace AutoPoco
{
    public static class StandardExtensions
    {
        /// <summary>
        /// Sets the value of a member directly
        /// </summary>
        public static IEngineConfigurationTypeBuilder<TPoco> Value<TPoco, TMember>(this IEngineConfigurationTypeMemberBuilder<TPoco, TMember> memberConfig,TMember value)
        {
            return memberConfig.Use<ValueSource<TMember>>(value);
        }
    }
}
