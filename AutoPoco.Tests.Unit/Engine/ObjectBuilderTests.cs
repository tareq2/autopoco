using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;
using Moq;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class ObjectBuilderTests
    {
        private IGenerationContext CreateDummyContext()
        {
            Mock<IGenerationContext> context = new Mock<IGenerationContext>();
            context.SetupGet(x => x.Node).Returns(new Mock<IGenerationContextNode>().Object);
            context.SetupGet(x => x.Builders).Returns(new Mock<IObjectBuilderRepository>().Object);
            return context.Object;
        }

        [Test]
        public void CreateObject_ReturnsObject()
        {
            Mock<IEngineConfigurationType> type = new Mock<IEngineConfigurationType>();
            type.SetupGet(x => x.RegisteredType).Returns(typeof(SimpleUser));

            ObjectBuilder builder = new ObjectBuilder(type.Object);
            SimpleUser user = builder.CreateObject(CreateDummyContext()) as SimpleUser;

            Assert.NotNull(user);
        }

        [Test]
        public void CreateObject_AppliesActionsToObject()
        {
            Mock<IEngineConfigurationType> type = new Mock<IEngineConfigurationType>();
            type.SetupGet(x => x.RegisteredType).Returns(typeof(SimpleUser));
            ObjectBuilder builder = new ObjectBuilder(type.Object);
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();

            Object obj = null;
            actionMock.Setup(x => x.Enact(It.IsAny<IGenerationContext>(), It.IsAny<Object>()))
                .Callback((IGenerationSession session, Object enactObject) =>
                {
                    obj = enactObject;
                });

            builder.AddAction(actionMock.Object);
            Object createdObject = builder.CreateObject(CreateDummyContext());

           Assert.AreEqual(obj, createdObject);
        }

        [Test]
        public void AddAction_AddsAction()
        {
            Mock<IEngineConfigurationType> type = new Mock<IEngineConfigurationType>();
            type.SetupGet(x => x.RegisteredType).Returns(typeof(SimpleUser));
            ObjectBuilder builder = new ObjectBuilder(type.Object);
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            builder.AddAction(actionMock.Object);

            Assert.AreEqual(1, builder.Actions.Count(x => x == actionMock.Object));
        }

        [Test]
        public void RemoveAction_RemovesAction()
        {
            Mock<IEngineConfigurationType> type = new Mock<IEngineConfigurationType>();
            type.SetupGet(x => x.RegisteredType).Returns(typeof(SimpleUser));
            ObjectBuilder builder = new ObjectBuilder(type.Object);
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            
            builder.AddAction(actionMock.Object);
            builder.RemoveAction(actionMock.Object);

            Assert.AreEqual(0, builder.Actions.Count(x => x == actionMock.Object));
        }


        [Test]
        public void ClearActions_RemovesAllActions()
        {
            Mock<IEngineConfigurationType> type = new Mock<IEngineConfigurationType>();
            type.SetupGet(x => x.RegisteredType).Returns(typeof(SimpleUser));
            ObjectBuilder builder = new ObjectBuilder(type.Object);
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            Mock<IObjectAction> actionMock2 = new Mock<IObjectAction>();

            builder.AddAction(actionMock.Object);
            builder.AddAction(actionMock2.Object);

            builder.ClearActions();

            Assert.AreEqual(0, builder.Actions.Count());
        }

        [Test]
        public void Create_Wraps_Context_With_TypeContext()
        {
            IObjectBuilderRepository builderRepository = new Mock<IObjectBuilderRepository>().Object;
            IGenerationContextNode parent = new Mock<IGenerationContextNode>().Object;
            GenerationContext context = new GenerationContext(builderRepository, parent);
            Mock<IObjectAction> actionMock = new Mock<IObjectAction>();
            ObjectBuilder builder = new ObjectBuilder(new EngineConfigurationType(typeof (SimpleUser)));

            builder.AddAction(actionMock.Object);
            builder.CreateObject(context);

            actionMock.Verify(
                x => x.Enact(It.Is<IGenerationContext>(y => y.Node is TypeGenerationContextNode), It.IsAny<SimpleUser>()), Times.Once());
        }
    }
}
