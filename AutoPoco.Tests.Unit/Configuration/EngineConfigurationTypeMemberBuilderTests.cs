using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Util;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationTypeMemberBuilderTests
    {
        [Test]
        public void Use_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

            IEngineConfigurationTypeBuilder<SimpleUser> returnedConfiguration = propertyConfiguration.Use<SimpleDataSource>();
            
            Assert.AreEqual(configuration, returnedConfiguration);           
        }

        [Test]
        public void UseWithArgs_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

            IEngineConfigurationTypeBuilder<SimpleUser> returnedConfiguration = propertyConfiguration.Use<SimpleDataSource>(0,1,10);

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void Default_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

            IEngineConfigurationTypeBuilder<SimpleUser> returnedConfiguration = propertyConfiguration.Default();

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void Default_ResetsSource()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

             propertyConfiguration.Use<SimpleDataSource>();
             propertyConfiguration.Default();

            Assert.IsNull(propertyConfiguration.GetDatasource());
        }

        [Test]
        public void GetConfigurationMember_ReturnsConfigurationMember()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(
                member, configuration);

           EngineTypeMember returnMember =  propertyConfiguration.GetConfigurationMember();
           Assert.AreEqual(member, returnMember);

        }

        [Test]
        public void GetConfigurationAction_Invalid_ReturnsNULL()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(
                member, configuration);

            IEngineConfigurationDatasource returnAction = propertyConfiguration.GetDatasource();
            Assert.Null(returnAction);
        }

        [Test]
        public void GetConfigurationAction_Valid_ReturnsConfigurationAction()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(
                member, configuration);
            propertyConfiguration.Use<SimpleDataSource>();

            IEngineConfigurationDatasource returnAction = propertyConfiguration.GetDatasource();
            Assert.NotNull(returnAction);
        }


    }
}
