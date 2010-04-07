using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface ICollectionContext<TPoco, TCollection> where TCollection : ICollection<TPoco>
    {
        /// <summary>
        /// Specifies that the first 'group' of items created have some specific properties
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionSelectionContext<TPoco, TCollection> First(int count);

        /// <summary>
        /// Specifies that a random number of items created have some specific properties
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionSelectionContext<TPoco, TCollection> Random(int count);

        /// <summary>
        /// Specifies that all of the items created have some specified properties
        /// </summary>
        /// <returns></returns>
        ICollectionSelectionContext<TPoco, TCollection> All();
    }
}
