using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Conventions;
using AutoPoco.Actions;

namespace AutoPoco.Engine
{
    public class GenerationSession : IGenerationSession
    {
        private IEngineConfiguration mConfiguration;
        private List<IObjectBuilder> mObjectBuilders = new List<IObjectBuilder>();

        public IEnumerable<IObjectBuilder> RegisteredTypes
        {
            get { return mObjectBuilders; }
        }

        public GenerationSession(IEngineConfiguration configuration)
        {
            mConfiguration = configuration;
            mObjectBuilders = new List<IObjectBuilder>();
        }

        public IObjectGenerator<TPoco> Single<TPoco>()
        {
            Type searchType = typeof(TPoco);
            IObjectBuilder foundType = GetBuilderForType(searchType);
            return new ObjectGenerator<TPoco>(this, foundType);
        }

        public ICollectionContext<TPoco, IList<TPoco>> List<TPoco>(int count)
        {
            return new CollectionContext<TPoco, IList<TPoco>>(
               Enumerable.Range(0, count)
                    .Select(x=> this.Single<TPoco>()).ToArray()
               .AsEnumerable());
        }

        private IObjectBuilder GetBuilderForType(Type searchType)
        {
            IObjectBuilder builder = mObjectBuilders.Where(x => x.InnerType == searchType).SingleOrDefault();
            if (builder == null)
            {
                builder = CreateBuilderForType(searchType);
            }
            return builder;
        }

        private IObjectBuilder CreateBuilderForType(Type searchType)
        {
            IEngineConfigurationType type = mConfiguration.GetRegisteredType(searchType);
            if (type == null)
            {
               type = RegisterTypeWithDefaults(searchType);
            }

            var builder = new ObjectBuilder(type);
            mObjectBuilders.Add(builder);
            return builder;
        }

        private IEngineConfigurationType RegisterTypeWithDefaults(Type searchType)
        {
            // This will probably get moved as I push more just-in-time configuration to the API
            // Perhaps expose this as something to be overriden "Deal with unknown types" delegate
            mConfiguration.RegisterType(searchType);
            var typeConfig = mConfiguration.GetRegisteredType(searchType);
            
            DefaultTypeConvention defaultConvention = new DefaultTypeConvention();
            defaultConvention.Apply(new TypeConventionContext(typeConfig));

            return typeConfig;

        }
    }
}
