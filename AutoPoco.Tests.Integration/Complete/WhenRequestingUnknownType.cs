using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenRequestingUnknownType
    {
        IGenerationSession mGenerationSession;

        [SetUp]
        public void SetupTest()
        {
           mGenerationSession = AutoPocoContainer.Configure(x => { }).CreateSession();
        }

        [Test]
        public void Valid_Object_Is_Returned()
        {
            SimpleUser user = mGenerationSession.Single<SimpleUser>().Get();
            Assert.NotNull(user);
        }
    }
}
