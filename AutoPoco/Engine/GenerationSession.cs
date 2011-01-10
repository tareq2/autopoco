using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Conventions;
using AutoPoco.Actions;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Engine
{
    public class GenerationSession : IGenerationSession
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventions;

        private List<IObjectBuilder> mObjectBuilders = new List<IObjectBuilder>();

        public IEnumerable<IObjectBuilder> RegisteredTypes
        {
            get { return mObjectBuilders; }
        }

        public GenerationSession(IEngineConfiguration configuration, IEngineConventionProvider conventionProvider)
        {
            mConfiguration = configuration;
            mConventions = conventionProvider;
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
            EnsureTypeExists(searchType);
            IEngineConfigurationType type = mConfiguration.GetRegisteredType(searchType);
            var builder = new ObjectBuilder(type);
            mObjectBuilders.Add(builder);
            return builder;
        }

        private void EnsureTypeExists(Type searchType)
        {
            if (mConfiguration.GetRegisteredType(searchType) != null) return;

            AdhocEngineConfigurationProvider provider = new AdhocEngineConfigurationProvider(new[] { searchType });
            var coreConvention = new DefaultEngineConfigurationProviderLoadingConvention();
            coreConvention.Apply(new EngineConfigurationProviderLoadingConventionContext(mConfiguration, provider, mConventions));      
        }
    }
}
