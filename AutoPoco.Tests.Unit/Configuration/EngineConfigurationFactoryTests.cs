using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using Moq;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationFactoryTests
    {
        [Test]
        public void Create_WithEmptySetup_ReturnsConfiguration()
        {

           var configurationProviderMock = new Mock<IEngineConfigurationProvider>();
           var conventionProviderMock = new Mock<IEngineConventionProvider>();
           var factory = new EngineConfigurationFactory();

           IEngineConfiguration configuration = factory.Create(
                configurationProviderMock.Object,
                conventionProviderMock.Object);

           Assert.NotNull(configuration);
        }

        [Test]
        public void CreateWithDerivedClassAndOneType_InvokesActionsOnType()
        {
            Mock<IEngineConfigurationFactoryTypeAction> testAction = new Mock<IEngineConfigurationFactoryTypeAction>();
            TestDerivedConfigurationFactory factory = new TestDerivedConfigurationFactory(
                new IEngineConfigurationFactoryTypeAction[] { testAction.Object });

            var configurationProviderMock = new Mock<IEngineConfigurationProvider>();
            var typeConfigurationTypeProviderMock = new Mock<IEngineConfigurationTypeProvider>();
            var conventionProviderMock = new Mock<IEngineConventionProvider>();

            configurationProviderMock.Setup(x => x.GetConfigurationTypes()).Returns(() =>
            {
                return new IEngineConfigurationTypeProvider[] { typeConfigurationTypeProviderMock.Object };
            });

            factory.Create(configurationProviderMock.Object, conventionProviderMock.Object);

            testAction.Verify(x =>
                x.Apply(It.IsAny<IEngineConfigurationType>()), 
                Times.Once());
        }

        public class TestDerivedConfigurationFactory : EngineConfigurationFactory
        {
            IEngineConfigurationFactoryTypeAction[] mActions;

            public TestDerivedConfigurationFactory(IEngineConfigurationFactoryTypeAction[] actions)
            {
                mActions = actions;
            }

            protected override IEnumerable<IEngineConfigurationFactoryTypeAction> CreateTypeActions(EngineConfiguration configuration, IEngineConfigurationProvider configurationProvider, IEngineConventionProvider conventionProvider)
            {
                return mActions;
            }
        }
    }
}
