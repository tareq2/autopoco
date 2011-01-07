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
    public class GenerationSessionTests
    {
        private GenerationSession mGenerationSession;

        [SetUp]
        public void TestSetup()
        {
            IEngineConfiguration configuration = new EngineConfiguration();
            configuration.RegisterType(typeof(SimpleUser));            
            mGenerationSession = new GenerationSession(configuration);
        }

        [Test]
        public void Single_ValidType_ReturnsObjectGenerator()
        {
           IObjectGenerator<SimpleUser> userGenerator = mGenerationSession.Single<SimpleUser>();
           Assert.NotNull(userGenerator);
        }

        [Test]
        public void Single_UnknownType_ReturnsObjectGenerator()
        {
            IObjectGenerator<SimpleUser> userGenerator = mGenerationSession.Single<SimpleUser>();
            Assert.NotNull(userGenerator);
        }

        [Test]
        public void List_ValidType_ReturnsCollectionContext()
        {
            ICollectionContext<SimpleUser, IList<SimpleUser>> userGenerator = mGenerationSession.List<SimpleUser>(10);

            Assert.NotNull(userGenerator);
        }

        [Test]
        public void List_UnknownType_ReturnsObjectGenerator()
        {
            ICollectionContext<SimpleUser, IList<SimpleUser>> userGenerator = mGenerationSession.List<SimpleUser>(10);
            Assert.NotNull(userGenerator);
        }
    }
}
