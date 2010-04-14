using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;

namespace AutoPoco
{
    public static class AutoPocoContainer
    {
        public static IGenerationSessionFactory Configure(Action<IEngineConfigurationBuilder> setup)
        {            
            var config = new EngineConfigurationBuilder();
            setup.Invoke(config);
            var configFactory = new DefaultEngineConfigurationFactory();
            return new GenerationSessionFactory(configFactory.Create(config, config.ConventionProvider));
        }
    }
}
