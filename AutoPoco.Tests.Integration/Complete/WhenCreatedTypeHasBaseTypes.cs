using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using AutoPoco;
using AutoPoco.Engine;
using AutoPoco.Testing;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenCreatedTypeHasBaseTypes
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.Include<ISimpleInterface>()
                    .Setup(c => c.InterfaceValue).Value("Test");
                 x.Include<SimpleBaseClass>()
                    .Setup(c => c.BaseProperty).Value("Test")
                    .Setup(c => c.BaseVirtualProperty).Value("Base");
                x.Include<SimpleDerivedClass>()
                    .Setup(c => c.Name).Value("OtherTest")
                    .Setup(c => c.BaseVirtualProperty).Value("Derived");

            })
            .CreateSession();
        }

        [Test]
        public void DerivedType_HasInterfaceValue()
        {
            SimpleDerivedClass derivedClass = mSession.Single<SimpleDerivedClass>().Get();
            Assert.AreEqual("Test", derivedClass.InterfaceValue);
        }

        [Test]
        public void DerivedType_OverriddenMember_HasDerivedValue()
        {
            SimpleDerivedClass derivedClass = mSession.Single<SimpleDerivedClass>().Get();
            Assert.AreEqual("Derived", derivedClass.BaseVirtualProperty);
        }

        [Test]
        public void DerivedType_ContainsBaseValues()
        {
            SimpleDerivedClass derivedClass = mSession.Single<SimpleDerivedClass>().Get();
            Assert.AreEqual("Test", derivedClass.BaseProperty);
        }
    }
}
