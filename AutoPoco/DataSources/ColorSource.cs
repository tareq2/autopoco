﻿using System;
using System.Collections.Generic;
using System.Drawing;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class ColorSource : DatasourceBase<Color>
    {
        private readonly int _max;
        private readonly Random _random;
        private readonly int _min;

        public ColorSource()
        {
            _random = new Random();
            _min = 0;
            _max = 255;
        }

        #region Overrides of DatasourceBase<Color>

        public override Color Next(IGenerationSession session)
        {
            return Color.FromArgb(
                _random.Next(_min, _max), 
                _random.Next(_min, _max), 
                _random.Next(_min, _max)
                );
        }

        #endregion
    }

    /// <summary>
    /// Allows you to use another Source to generate an enumerable collection
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class EnumerableSource<TSource, T> : DatasourceBase<IEnumerable<T>>
    where TSource : IDatasource<T>
    {
        private readonly int _count;
        private readonly object[] _args;
        private readonly IDatasource<T> _source;

        public EnumerableSource(int count)
            : this(count, new object[] { })
        { }

        public EnumerableSource(int count, params object[] args)
        {
            _count = count;
            _args = args;

            var factory = new DatasourceFactory(typeof(TSource));
            factory.SetParams(_args);
            _source = (IDatasource<T>)factory.Build();
        }

        public override IEnumerable<T> Next(IGenerationSession session)
        {
            for (var i = 0; i < _count; i++)
                yield return (T)_source.Next(session);
        }
    }
}