using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationTypeBuilderTests
    {
        [Test]
        public void Setup_WithProperty_ReturnsMemberConfiguration()
        {
            EngineConfigurationTypeBuilder<SimplePropertyClass> configuration = new EngineConfigurationTypeBuilder<SimplePropertyClass>();
            IEngineConfigurationTypeMemberBuilder<SimplePropertyClass, String> memberConfiguration = configuration.Setup(x => x.SomeProperty);

            Assert.NotNull(memberConfiguration);
        }

        [Test]
        public void Setup_WithField_ReturnsMemberConfiguration()
        {
            EngineConfigurationTypeBuilder<SimpleFieldClass> configuration = new EngineConfigurationTypeBuilder<SimpleFieldClass>();
            IEngineConfigurationTypeMemberBuilder<SimpleFieldClass, String> memberConfiguration = configuration.Setup(x => x.SomeField);

            Assert.NotNull(memberConfiguration);
        }

        [Test]
        public void GetConfigurationType_ReturnsType()
        {
            EngineConfigurationTypeBuilder<SimpleFieldClass> configuration = new EngineConfigurationTypeBuilder<SimpleFieldClass>();
            Type type = configuration.GetConfigurationType();

            Assert.AreEqual(typeof(SimpleFieldClass), type);
        }

        [Test]
        public void GetConfigurationMembers_ReturnsMembers()
        {
            EngineConfigurationTypeBuilder<SimpleFieldClass> configuration = new EngineConfigurationTypeBuilder<SimpleFieldClass>();

            configuration.Setup(x => x.SomeField);
            configuration.Setup(x => x.SomeOtherField);

            var members = configuration.GetConfigurationMembers();

            Assert.AreEqual(2, members.Count());
        }
    }
}
