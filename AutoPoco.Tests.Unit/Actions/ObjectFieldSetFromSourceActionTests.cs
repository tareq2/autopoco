using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Actions;
using AutoPoco.Util;
using AutoPoco.Testing;
using AutoPoco.Configuration;
using Moq;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectFieldSetFromSourceActionTests
    {
        Mock<IDatasource> mSourceMock;
        Mock<IGenerationSession> mSessionMock;
        ObjectFieldSetFromSourceAction mAction;

        [SetUp]
        public void SetupObjects()
        {
            mSourceMock = new Mock<IDatasource>();
            mSessionMock = new Mock<IGenerationSession>();
            mAction = new ObjectFieldSetFromSourceAction((EngineTypeFieldMember)
               ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField), mSourceMock.Object);
        }

        [Test]
        public void Enact_SetsFieldWithSourceValue()
        {
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationSession>())).Returns("Test");

            SimpleFieldClass target = new SimpleFieldClass();
            mAction.Enact(mSessionMock.Object, target);

            Assert.AreEqual("Test", target.SomeField);
        }

        [Test]
        public void Enact_ProvidesSourceWithSession()
        {
            SimpleFieldClass target = new SimpleFieldClass();
            mAction.Enact(mSessionMock.Object, target);

            mSourceMock.Verify(x => x.Next(It.Is<IGenerationSession>(y => y == mSessionMock.Object)), Times.Once());
        }
    }
}
