using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class GenerationSessionTests
    {
        private GenerationSession mGenerationSession;

        [SetUp]
        public void TestSetup()
        {
            mGenerationSession = new GenerationSession(
                new ObjectBuilder[] {
                        new ObjectBuilder(typeof(SimpleUser))
                    }
                );
        }

        [Test]
        public void Single_ValidType_ReturnsObjectGenerator()
        {
           IObjectGenerator<SimpleUser> userGenerator = mGenerationSession.Single<SimpleUser>();
           Assert.NotNull(userGenerator);
        }

        [Test]
        public void Single_InvalidType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                mGenerationSession.Single<SimplePropertyClass>();
            });
        }

        [Test]
        public void List_ValidType_ReturnsCollectionContext()
        {
            ICollectionContext<SimpleUser, IList<SimpleUser>> userGenerator = mGenerationSession.List<SimpleUser>(10);

            Assert.NotNull(userGenerator);
        }

        [Test]
        public void List_InvalidType_ReturnsCollectionContext()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                mGenerationSession.List<SimplePropertyClass>(10);
            });
        }
    }
}
