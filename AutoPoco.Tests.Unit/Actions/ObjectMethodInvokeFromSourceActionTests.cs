using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using Moq;
using AutoPoco.Actions;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using AutoPoco.Util;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectMethodInvokeFromSourceActionTests
    {
        Mock<IDatasource> mSourceMock;
        Mock<IGenerationSession> mSessionMock;
        ObjectMethodInvokeFromSourceAction mDoubleArgAction;
        EngineTypeMethodMember mDoubleArgMethod;

        [SetUp]
        public void SetupObjects()
        {
            mSourceMock = new Mock<IDatasource>();
            mSessionMock = new Mock<IGenerationSession>();

            mDoubleArgMethod = (EngineTypeMethodMember)ReflectionHelper.GetMember(typeof(SimpleMethodClass).GetMethod("SetSomething", new Type[] { typeof(string), typeof(string) }));

            mDoubleArgAction = new ObjectMethodInvokeFromSourceAction(mDoubleArgMethod, new IDatasource[] { mSourceMock.Object, mSourceMock.Object });
        }


        [Test]
        public void SharedDataSourceWithTwoParams_NextInvokedWithSessionTwice()
        {
            SimpleMethodClass target = new SimpleMethodClass();
            mDoubleArgAction.Enact(mSessionMock.Object, target);

            mSourceMock.Verify(x => x.Next(It.Is<IGenerationSession>(y => y == mSessionMock.Object)), Times.Exactly(2));
        }

        [Test]
        public void SharedDataSourceWithTwoParams_FirstParamPassedCorrectly()
        {
            SimpleMethodClass target = new SimpleMethodClass();
            int callCount = 0;
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationSession>())).Returns(() =>
            {
                callCount++;
                return callCount.ToString();
            });

            mDoubleArgAction.Enact(mSessionMock.Object, target);


            Assert.AreEqual("1", target.Value);
        }

        [Test]
        public void SharedDataSourceWithTwoParams_SecondParamPassedCorrectly()
        {
            SimpleMethodClass target = new SimpleMethodClass();
            int callCount = 0;
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationSession>())).Returns(() =>
            {
                callCount++;
                return callCount.ToString();
            });

            mDoubleArgAction.Enact(mSessionMock.Object, target);


            Assert.AreEqual("2", target.OtherValue);
        }



    }
}
