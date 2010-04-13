using System;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class GuidSourceTests
    {
        [Test]
        public void Next_Returns_A_Guid()
        {
            GuidSource source = new GuidSource();
            var value = source.Next(null);
            Assert.IsFalse(value.CompareTo(Guid.Empty) == 0);
        }
    }
}