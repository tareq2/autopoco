using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;
using AutoPoco.Conventions;

namespace AutoPoco
{
    public static class AutoPocoContainer
    {
        public static IGenerationSessionFactory Configure(Action<IEngineConfigurationBuilder> setup)
        {            
            var config = new EngineConfigurationBuilder();
            // This might seem a bit odd, but it is crucial behaviour and used to just be hard coded in (not a breaking change)
            config.Conventions(x => x.Register<DefaultEngineConfigurationProviderLoadingConvention>());
            setup.Invoke(config);
            var configFactory = new EngineConfigurationFactory();
            return new GenerationSessionFactory(configFactory.Create(config, config.ConventionProvider), config.ConventionProvider);
        }

        public static IGenerationSession CreateDefaultSession()
        {
            var config = new EngineConfigurationBuilder();
            var configFactory = new EngineConfigurationFactory();

            config.Conventions(x => x.UseDefaultConventions());
        
            return new GenerationSessionFactory(
                configFactory.Create(config, config.ConventionProvider), config.ConventionProvider)
                .CreateSession();
        }
    }
}
