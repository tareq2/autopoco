using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using AutoPoco.Engine;
using AutoPoco.Actions;
using AutoPoco.Util;
using AutoPoco.Testing;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectPropertySetFromSourceActionTests
    {
        Mock<IDatasource> mSourceMock;
        Mock<IGenerationSession> mSessionMock;
        ObjectPropertySetFromSourceAction mAction;

        [SetUp]
        public void SetupObjects()
        {
            mSourceMock = new Mock<IDatasource>();
            mSessionMock = new Mock<IGenerationSession>();
            mAction = new ObjectPropertySetFromSourceAction((EngineTypePropertyMember)
               ReflectionHelper.GetMember<SimplePropertyClass>(x => x.SomeProperty), mSourceMock.Object);
        }

        [Test]
        public void Enact_SetsFieldWithSourceValue()
        {
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationSession>())).Returns("Test");

            SimplePropertyClass target = new SimplePropertyClass();
            mAction.Enact(mSessionMock.Object, target);

            Assert.AreEqual("Test", target.SomeProperty);
        }

        [Test]
        public void Enact_ProvidesSourceWithSession()
        {
            SimplePropertyClass target = new SimplePropertyClass();
            mAction.Enact(mSessionMock.Object, target);

            mSourceMock.Verify(x => x.Next(It.Is<IGenerationSession>(y => y == mSessionMock.Object)), Times.Once());
        }
    }
}
