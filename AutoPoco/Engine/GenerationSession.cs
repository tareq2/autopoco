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

        public virtual IObjectGenerator<TPoco> Single<TPoco>()
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

        public TPoco Next<TPoco>()
        {
            return this.Single<TPoco>().Get();
        }

        public TPoco Next<TPoco>(Action<IObjectGenerator<TPoco>> cfg)
        {
            var generator = this.Single<TPoco>();
            cfg.Invoke(generator);
            return generator.Get();
        }

        public IEnumerable<TPoco> Collection<TPoco>(int count)
        {
            var generator = this.List<TPoco>(count);
            return generator.Get();
        }

        public IEnumerable<TPoco> Collection<TPoco>(int count, Action<ICollectionContext<TPoco, IList<TPoco>>> cfg)
        {
            var generator = this.List<TPoco>(count);
            cfg.Invoke(generator);
            return generator.Get();
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
            coreConvention.Apply(new EngineConfigurationProviderLoaderContext(mConfiguration, provider, mConventions));      
        }
    }
}
