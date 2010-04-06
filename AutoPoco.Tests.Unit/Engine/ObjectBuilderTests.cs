using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;
using Moq;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class ObjectBuilderTests
    {
        [Test]
        public void CreateObject_ReturnsObject()
        {
            ObjectBuilder builder = new ObjectBuilder(typeof(SimpleUser));
            SimpleUser user = builder.CreateObject(null) as SimpleUser;

            Assert.NotNull(user);
        }

        [Test]
        public void CreateObject_AppliesActionsToObject()
        {
            ObjectBuilder builder = new ObjectBuilder(typeof(SimpleUser));
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();

            Object obj = null;
            actionMock.Setup(x => x.Enact(null, It.IsAny<Object>()))
                .Callback((IGenerationSession session, Object enactObject) =>
                {
                    obj = enactObject;
                });

            builder.AddAction(actionMock.Object);
            Object createdObject = builder.CreateObject(null);

           Assert.AreEqual(obj, createdObject);
        }

        [Test]
        public void AddAction_AddsAction()
        {
            ObjectBuilder builder = new ObjectBuilder(typeof(SimpleUser));
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            builder.AddAction(actionMock.Object);

            Assert.AreEqual(1, builder.Actions.Count(x => x == actionMock.Object));
        }

        [Test]
        public void RemoveAction_RemovesAction()
        {
            ObjectBuilder builder = new ObjectBuilder(typeof(SimpleUser));
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            
            builder.AddAction(actionMock.Object);
            builder.RemoveAction(actionMock.Object);

            Assert.AreEqual(0, builder.Actions.Count(x => x == actionMock.Object));
        }


        [Test]
        public void ClearActions_RemovesAllActions()
        {
            ObjectBuilder builder = new ObjectBuilder(typeof(SimpleUser));
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            Mock<IObjectAction> actionMock2 = new Mock<IObjectAction>();

            builder.AddAction(actionMock.Object);
            builder.AddAction(actionMock2.Object);

            builder.ClearActions();

            Assert.AreEqual(0, builder.Actions.Count());
        }

    }
}
