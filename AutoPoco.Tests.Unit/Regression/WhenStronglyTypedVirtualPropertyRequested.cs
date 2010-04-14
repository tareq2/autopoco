using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Util;

namespace AutoPoco.Tests.Unit.Regression
{
    [TestFixture]
    public class WhenStronglyTypedVirtualPropertyRequested
    {
        EngineTypePropertyMember mMember;

        [SetUp]
        public void SetupObjects()
        {
            mMember = (EngineTypePropertyMember)ReflectionHelper.GetMember<DerivedClass>(x => x.SomeProperty);
        }

        [Test]
        public void RequestedProperty_IsOverriddenProperty()
        {
            Assert.AreEqual(typeof(DerivedClass), mMember.PropertyInfo.DeclaringType);
        }

        public class BaseClass
        {
            public virtual string SomeProperty
            {
                get;
                set;
            }
        }

        public class DerivedClass : BaseClass
        {
            public override string  SomeProperty
            {
	            get;
                set;
            }
        }
    }
}
