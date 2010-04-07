using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public class GenerationSession : IGenerationSession
    {
        public IEnumerable<IObjectBuilder> RegisteredTypes
        {
            get;
            private set;
        }

        public GenerationSession(IObjectBuilder[] types)
        {
            this.RegisteredTypes = types;
        }

        public IObjectGenerator<TPoco> Single<TPoco>()
        {
            Type searchType = typeof(TPoco);
            IObjectBuilder foundType = RegisteredTypes.Where(x => x.InnerType == searchType).SingleOrDefault();
            if (foundType == null) { throw new ArgumentException("Unrecognised type requested", "T"); }
            return new ObjectGenerator<TPoco>(this, foundType);
        }

        public ICollectionContext<TPoco, IList<TPoco>> List<TPoco>(int count)
        {
            Type searchType = typeof(TPoco);
            IObjectBuilder foundType = RegisteredTypes.Where(x => x.InnerType == searchType).SingleOrDefault();
            if (foundType == null) { throw new ArgumentException("Unrecognised type requested", "T"); }

            return new CollectionContext<TPoco, IList<TPoco>>(
               Enumerable.Range(0, count)
                    .Select(x=> this.Single<TPoco>()).ToArray()
               .AsEnumerable());
        }
    }
}
