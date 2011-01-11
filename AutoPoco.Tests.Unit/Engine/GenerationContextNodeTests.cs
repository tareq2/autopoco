using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using Moq;
using AutoPoco.Configuration;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class GenerationContextNodeTests
    {
        [Test]
        public void Default_Ctor_Sets_Properties_From_Arguments()
        {
            Mock<IGenerationContextNode> site = new Mock<IGenerationContextNode>();
            Object targetObject = new object();
            EngineTypeMember targetMember = new EngineTypePropertyMember(typeof (SimpleBaseClass).GetProperty("BaseProperty"));
            GenerationContextNode node = new GenerationContextNode(site.Object, targetObject, targetMember);

            Assert.AreEqual(site.Object, node.Site);
            Assert.AreEqual(targetObject, node.TargetObject);
            Assert.AreEqual(targetMember, node.TargetMember);
        }
    }
}
