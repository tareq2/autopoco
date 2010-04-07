using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationBuilder
    {
        /// <summary>
        /// Registers a type with the configuration and allows further configuration of that type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder<T> Include<T>() where T : new();

        /// <summary>
        /// Registeres a type with the configuration and allows further configuration of that type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder Include(Type t);

        /// <summary>
        /// Sets up the conventions that the engine will use
        /// </summary>
        void Conventions(Action<IEngineConventionConfiguration> config);   
    }
}
