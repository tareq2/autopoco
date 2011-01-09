using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class TypeRegistrationConventionContext : ITypeRegistrationConventionContext
    {
        public IEngineConfiguration Configuration
        {
            get;
            private set;
        }

        public IEngineConfigurationProvider ConfigurationProvider
        {
            get;
            private set;
        }

        public IEngineConventionProvider ConventionProvider
        {
            get;
            private set;
        }

        public IEngineConfigurationType Target
        {
            get;
            private set;
        }

        public TypeRegistrationConventionContext(
            IEngineConfiguration configuration, 
            IEngineConfigurationProvider configurationProvider, 
            IEngineConventionProvider conventionProvider,
            IEngineConfigurationType engineConfigurationType)
        {
            this.Configuration = configuration;
            this.ConfigurationProvider = configurationProvider;
            this.ConventionProvider = conventionProvider;
            this.Target = engineConfigurationType;
        }
    }
}
