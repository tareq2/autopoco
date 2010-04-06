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
        public void With_ValidType_ReturnsObjectGenerator()
        {
           IObjectGenerator<SimpleUser> userGenerator = mGenerationSession.With<SimpleUser>();
           Assert.NotNull(userGenerator);
        }

        [Test]
        public void With_InvalidType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                mGenerationSession.With<SimplePropertyClass>();
            });
        }
    }
}
