using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public interface IObjectGenerator<TPoco>
    {
        /// <summary>
        /// Creates an instance of an object conforming to the specified rules
        /// </summary>
        /// <returns></returns>
        TPoco Get();

        /// <summary>
        /// Creates a list of objects conforming to the specified values
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionContext<TPoco, IList<TPoco>> List(int count);

        /// <summary>
        /// Imposes a value on the created object that overrides any rules speciifed in configuration
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="propertyExpr"></param>
        /// <returns></returns>
        IObjectGenerator<TPoco> Impose<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, TMember value);
    }
}
