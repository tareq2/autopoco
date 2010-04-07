using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public interface ICollectionSelectionContext<TPoco, TCollection> where TCollection : ICollection<TPoco>
    {
        /// <summary>
        /// Imposes a value on the created object that overrides any rules specified in the configuration
        /// </summary>
        ICollectionSelectionContext<TPoco, TCollection> Impose<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, TMember value);

    }
}
