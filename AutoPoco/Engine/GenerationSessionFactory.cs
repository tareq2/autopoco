using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Actions;

namespace AutoPoco.Engine
{
    public class GenerationSessionFactory : IGenerationSessionFactory
    {
        private IEngineConfiguration mConfig;
        public GenerationSessionFactory(IEngineConfiguration config)
        {
            mConfig = config;
        }

        public IGenerationSession CreateSession()
        {
            //List<ObjectBuilder> builders = new List<ObjectBuilder>();
            //foreach(var type in mConfig.GetRegisteredTypes())
            //{
            //    // Create the builder around the type
            //    var builder = new ObjectBuilder(type);
            //    builders.Add(builder);
            //}

            // TODO: Need to deep-clone the config
            return new GenerationSession(mConfig);
        }
    }
}
