using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IGenerationSession
    {
        /// <summary>
        /// Sets up the generation of a single item of the specified type
        /// </summary>
        /// <typeparam name="TPoco"></typeparam>
        /// <returns></returns>
        IObjectGenerator<TPoco> Single<TPoco>();

        /// <summary>
        /// Sets up the generation of a list of items of the specified type
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionContext<TPoco, IList<TPoco>> List<TPoco>(int count);
    }
}
