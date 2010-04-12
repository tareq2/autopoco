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
            List<ObjectBuilder> builders = new List<ObjectBuilder>();
            foreach(var type in mConfig.GetRegisteredTypes())
            {
                // Create the builder around the type
                var builder = new ObjectBuilder(type.RegisteredType);

                // Create actions around properties/fields/methods
                type.GetRegisteredMembers()
                    .ToList()
                    .ForEach(x =>
                    {
                        var sources = x.GetDatasources().Select(s=>s.Build()).ToList();

                        if (x.Member.IsField)
                        {
                            if(sources.Count == 0) {return;}

                            builder.AddAction(new ObjectFieldSetFromSourceAction(
                               (EngineTypeFieldMember)x.Member,
                               sources.First()));
                        }
                        else if (x.Member.IsProperty)
                        {
                            if(sources.Count == 0) {return;}
                            
                            builder.AddAction(new ObjectPropertySetFromSourceAction(
                               (EngineTypePropertyMember)x.Member,
                               sources.First()));
                        }
                        else if (x.Member.IsMethod)
                        {
                            builder.AddAction(new ObjectMethodInvokeFromSourceAction(
                               (EngineTypeMethodMember)x.Member,
                               sources
                               ));
                        }
                    });

                builders.Add(builder);
            }

            return new GenerationSession(builders.ToArray());
        }
    }
}
