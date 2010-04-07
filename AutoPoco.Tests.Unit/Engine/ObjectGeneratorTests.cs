using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using AutoPoco.Engine;
using AutoPoco.Util;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using Moq;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class ObjectGeneratorTests
    {
        string mTestPropertyValue = "TestValue";
        ObjectGenerator<SimpleUser> mTestGenerator;

        [SetUp]
        public void TestSetup()
        {
            var builder = new ObjectBuilder(typeof(SimpleUser));
            builder.AddAction(
                new ObjectPropertySetFromSourceAction(
                    ReflectionHelper.GetMember<SimpleUser>(x=>x.FirstName) as EngineTypePropertyMember,
                    new SimpleDataSource(mTestPropertyValue)
                    ));
            builder.AddAction(
                new ObjectPropertySetFromSourceAction(
                    ReflectionHelper.GetMember<SimpleUser>(x => x.LastName) as EngineTypePropertyMember,
                    new SimpleDataSource(mTestPropertyValue)
                    ));
            builder.AddAction(
                new ObjectPropertySetFromSourceAction(
                    ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress) as EngineTypePropertyMember,
                    new SimpleDataSource(mTestPropertyValue)
                    ));

            mTestGenerator = new ObjectGenerator<SimpleUser>(null,builder);               
        }

        [Test]
        public void Get_ReturnsSingleObject()
        {
            SimpleUser user = mTestGenerator.Get();
            Assert.AreEqual(mTestPropertyValue, user.EmailAddress);
            Assert.AreEqual(mTestPropertyValue, user.FirstName);
            Assert.AreEqual(mTestPropertyValue, user.LastName);
        }

        [Test]
        public void Get_AppliesExtraActions()
        {
            Mock<IObjectAction> action = new Mock<IObjectAction>();
            Object actionObject = null;
            action.Setup(x => x.Enact(null, It.IsAny<Object>()))
                .Callback((IGenerationSession session, Object dest) =>
                {
                    actionObject = dest;
                });
            mTestGenerator.AddAction(action.Object);
            SimpleUser user = mTestGenerator.Get();

            Assert.AreEqual(actionObject, user);
        }

        [Test]
        public void Impose_OverridesDataSource()
        {
            String newValue = "SomethingElse";
            SimpleUser user = mTestGenerator.Impose(x => x.EmailAddress, newValue).Get();

            Assert.AreEqual(newValue, user.EmailAddress);
            Assert.AreEqual(mTestPropertyValue, user.FirstName);
            Assert.AreEqual(mTestPropertyValue, user.LastName);
        }

        [Test]
        public void Impose_ReturnsGenerator()
        {
             IObjectGenerator<SimpleUser> generator = mTestGenerator.Impose(x => x.EmailAddress, "");

             Assert.AreEqual(mTestGenerator, generator);
        }
    }
}
