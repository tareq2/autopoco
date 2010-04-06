using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Integration.Configuration
{
    [TestFixture]
    public class WhenTypeConventionApplied : ConfigurationBaseTest
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConfigurationType mType;

        [SetUp]
        public void Setup()
        {
            this.Builder.Conventions(x =>
            {
                x.Register<TestTypeConvention>();
            });
            this.Builder.Include<TestTypeClass>();

            mConfiguration = this.Builder.Build();
            mType = mConfiguration.GetRegisteredType(typeof(TestTypeClass));
        }

        [Test]
        public void ConfigurationContainsMemberOnRegisteredType()
        {
            Assert.AreEqual(1, mType.GetRegisteredMembers().Where(x => x.Member.Name == "Test").Count());
        }

        public class TestTypeClass
        {
            public string Test;
        }

        public class TestTypeConvention : ITypeConvention
        {
            public void Apply(ITypeConventionContext context)
            {
                context.RegisterField(typeof(TestTypeClass).GetField("Test"));
            }
        }
    }
}
