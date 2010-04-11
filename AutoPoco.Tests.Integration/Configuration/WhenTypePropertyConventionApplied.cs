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
    public class WhenTypePropertyConventionApplied : ConfigurationBaseTest
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConfigurationType mType;
        private IEngineConfigurationTypeMember mProperty;

        [SetUp]
        public void Setup()
        {
            this.Builder.Conventions(x =>
            {
                x.Register<TestPropertyConvention>();
            });
            this.Builder.Include<TestPropertyClass>().Setup(x => x.Test);

            mConfiguration = new EngineConfigurationFactory().Create(this.Builder, this.Builder.ConventionProvider);
            mType = mConfiguration.GetRegisteredType(typeof(TestPropertyClass));
            mProperty = mType.GetRegisteredMembers().Where(x => x.Member.Name == "Test").Single();
        }


        [Test]
        public void PropertySourceIsSetFromConvention()
        {
            var source = mProperty.GetDatasources().First().Build();
            Assert.AreEqual(typeof(TestDataSource), source.GetType());
        }

        public class TestPropertyClass
        {
            public string Test
            {
                get;
                set;
            }
        }

        public class TestPropertyConvention : ITypePropertyConvention
        {
            public void Apply(ITypePropertyConventionContext context)
            {
                context.SetSource<TestDataSource>();
            }

            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
                throw new NotImplementedException();
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
