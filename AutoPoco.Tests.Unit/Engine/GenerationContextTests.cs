using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using Moq;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class GenerationContextTests 
    {
        [Test]
        public void DatasourceContext_Default_Ctor_Sets_Properties()
        {
            Mock<IGenerationSession> session = new Mock<IGenerationSession>();
            Mock<IGenerationContextNode> site = new Mock<IGenerationContextNode>();
            IGenerationContext sourceContext = new GenerationContext(session.Object, site.Object);

            Assert.AreEqual(session.Object, sourceContext.Session);
            Assert.AreEqual(site.Object, sourceContext.Site);
        }
    }
}
