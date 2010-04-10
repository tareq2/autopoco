using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Integration.Configuration
{
    [TestFixture]
    public class WhenTypeFieldConventionApplied : ConfigurationBaseTest
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConfigurationType mType;
        private IEngineConfigurationTypeMember mField;

        [SetUp]
        public void Setup()
        {
            this.Builder.Conventions(x =>
            {
                x.Register<TestPropertyConvention>();
            });
            this.Builder.Include<TestFieldClass>().Setup(x => x.Test);

            mConfiguration = this.Builder.Build();
            mType = mConfiguration.GetRegisteredType(typeof(TestFieldClass));
            mField = mType.GetRegisteredMembers().Where(x => x.Member.Name == "Test").Single();
        }


        [Test]
        public void PropertySourceIsSetFromConvention()
        {
            var source = mField.GetDatasources().First().Build();
            Assert.AreEqual(typeof(TestDataSource), source.GetType());
        }

        public class TestFieldClass
        {
            public string Test;
        }

        public class TestPropertyConvention : ITypeFieldConvention
        {
            public void Apply(ITypeFieldConventionContext context)
            {
                context.SetSource<TestDataSource>();
            }
        }

        public class TestDataSource : IDatasource
        {
            public object Next(IGenerationSession session)
            {
                return null;
            }
        }
    }
}
