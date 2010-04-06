using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Engine;
using Moq;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class GenerationSessionFactoryTests
    {
        [Test]
        public void CreateSession_EmptyConfig_ReturnsSession()
        {
            GenerationSessionFactory config = new GenerationSessionFactory(new EngineConfiguration());
            IGenerationSession session = config.CreateSession();

            Assert.NotNull(session);
        }
    }
}
