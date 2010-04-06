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

        public IObjectGenerator<T> With<T>()
        {
            Type searchType = typeof(T);
            IObjectBuilder foundType = RegisteredTypes.Where(x => x.InnerType == searchType).SingleOrDefault();
            if (foundType == null) { throw new ArgumentException("Unrecognised type requested", "T"); }
            return new ObjectGenerator<T>(this, foundType);
        }
    }
}
