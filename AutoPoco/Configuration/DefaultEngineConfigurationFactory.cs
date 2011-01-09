using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    /// <summary>
    /// This factory is the default for AutoPoco, and wraps up the base factory with some additional behaviour to scan through the registered types
    /// and bind together members from base classes and interfaces into derived types so behaviour defined for those base types/interfaces will propogate into those derived types
    /// </summary>
    public class DefaultEngineConfigurationFactory : EngineConfigurationFactory
    {
        public override IEngineConfiguration Create(Providers.IEngineConfigurationProvider configurationProvider, Providers.IEngineConventionProvider conventionProvider)
        {
            // Create the default configuration
            return base.Create(configurationProvider, conventionProvider);
        }
    }
}
