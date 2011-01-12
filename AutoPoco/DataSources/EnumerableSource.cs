using System.Collections.Generic;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    /// <summary>
    /// Allows you to use another Source to generate an enumerable collection
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class EnumerableSource<TSource, T> : DatasourceBase<IEnumerable<T>>
        where TSource : IDatasource<T>
    {
        private readonly int mCount;
        private readonly object[] mArgs;
        private readonly IDatasource<T> mSource;

        public EnumerableSource(int count)
            : this(count, new object[] { })
        { }

        public EnumerableSource(int count, params object[] args)
        {
            mCount = count;
            mArgs = args;

            var factory = new DatasourceFactory(typeof(TSource));
            factory.SetParams(mArgs);
            mSource = (IDatasource<T>)factory.Build();
        }

        public override IEnumerable<T> Next(IGenerationContext context)
        {
            for (var i = 0; i < mCount; i++)
                yield return (T)mSource.Next(context);
        }
    }
}