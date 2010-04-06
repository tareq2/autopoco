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
                        var source = x.GetSource();
                        if(source == null) return;

                        if (x.Member.IsField)
                        {
                            builder.AddAction(new ObjectFieldSetFromSourceAction(
                               (EngineTypeFieldMember)x.Member,
                               source.Build()));
                        }
                        else if (x.Member.IsProperty)
                        {
                            builder.AddAction(new ObjectPropertySetFromSourceAction(
                               (EngineTypePropertyMember)x.Member,
                               source.Build()));
                        }
                    });

                builders.Add(builder);
            }

            return new GenerationSession(builders.ToArray());
        }
    }
}
