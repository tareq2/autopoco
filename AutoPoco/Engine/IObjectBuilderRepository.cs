using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IObjectBuilderRepository
    {
        /// <summary>
        /// Gets the object builder for a certain type
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        IObjectBuilder GetBuilderForType(Type searchType);
    }
}
