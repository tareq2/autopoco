using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public interface ITypeRegistrationConventionContext : IConvention
    {
        IEngineConfiguration Configuration { get; }
        IEngineConfigurationProvider ConfigurationProvider { get; }
        IEngineConventionProvider ConventionProvider { get; }
        IEngineConfigurationType Target { get; }
    }
}
