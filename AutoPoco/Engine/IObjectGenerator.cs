using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public interface IObjectGenerator<T>
    {
        /// <summary>
        /// Creates an instance of an object conforming to the specified rules
        /// </summary>
        /// <returns></returns>
        T Get();

        /// <summary>
        /// Creates an array of objects conforming to the specified values
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        T[] Get(int count);

        /// <summary>
        /// Imposes a value on the created object that overrides any rules speciifed in configuration
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="propertyExpr"></param>
        /// <returns></returns>
        IObjectGenerator<T> Impose<TMember>(Expression<Func<T, TMember>> propertyExpr, TMember value);
    }
}
