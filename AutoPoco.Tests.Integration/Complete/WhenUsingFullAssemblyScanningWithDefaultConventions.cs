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
    public class WhenUsingFullAssemblyScanningWithDefaultConventions
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            // As default as it gets
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.AddFromAssemblyContainingType<SimpleUser>();
            })
            .CreateSession();
        }

        [Test]
        public void SimpleUser_CanBeCreated()
        {
            SimpleUser user = mSession.With<SimpleUser>().Get();
            Assert.NotNull(user);
        }

        [Test]
        public void SimpleFieldClass_CanBeCreated()
        {
            SimpleFieldClass obj = mSession.With<SimpleFieldClass>().Get();
            Assert.NotNull(obj);
        }

        [Test]
        public void SimplePropertyClass_CanBeCreated()
        {
            SimplePropertyClass obj = mSession.With<SimplePropertyClass>().Get();
            Assert.NotNull(obj);
        }
    }
   
}
