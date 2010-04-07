using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Testing;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Integration.Complete
{
    public class WhenUsingDefaultConventionsWithExplicitSetup
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });

                x.Include<SimpleUser>()
                    .Setup(c => c.EmailAddress).Use<EmailAddressSource>()
                    .Setup(c => c.FirstName).Use<FirstNameSource>()
                    .Setup(c => c.LastName).Use<LastNameSource>();

                x.Include<SimpleUserRole>()
                    .Setup(c => c.Name).Random(5, 10);

                x.Include<SimpleFieldClass>();
                x.Include<SimplePropertyClass>();
                x.Include<DefaultPropertyClass>();
                x.Include<DefaultFieldClass>();
            })
            .CreateSession();
        }

        [Test]
        public void Get_SimpleUserRole_HasRandomName()
        {
            SimpleUserRole role = mSession.With<SimpleUserRole>().Get();
            Assert.GreaterOrEqual(role.Name.Length, 5);
            Assert.LessOrEqual(role.Name.Length, 10);
        }

        [Test]
        public void Get_SimpleUser_HasValidEmailAddress()
        {
           SimpleUser user = mSession.With<SimpleUser>().Get();
           Assert.IsTrue(user.EmailAddress.Contains("@"));
        }

        [Test]
        public void Get_SimpleSeveralUsers_HaveUniqueEmailAddresses()
        {
            throw new NotImplementedException();
            SimpleUser[] users = null; // mSession.With<SimpleUser>().Get();

            Assert.True(
                users.Where(x => users.Count(y => y.EmailAddress == x.EmailAddress) > 1).Count() == 0);
        }

        [Test]
        public void Get_SimpleUser_ImposeCustomEmailAddress_HasCustomEmailAddress()
        {
            SimpleUser user = mSession.With<SimpleUser>()
                .Impose(x => x.EmailAddress, "override@override.com")
                .Get();

            Assert.AreEqual("override@override.com", user.EmailAddress);
        }

        [Test]
        public void Get_SimpleUser_HasValidFirstName()
        {
            SimpleUser user = mSession.With<SimpleUser>().Get();
            Assert.IsTrue(user.FirstName.Length > 2);
        }

        [Test]
        public void Get_SimpleUser_HasValidLastName()
        {
            SimpleUser user = mSession.With<SimpleUser>().Get();
            Assert.IsTrue(user.LastName.Length > 2);
        }

        [Test]
        public void SimpleFieldClass_SomePropertyNotNull()
        {
            SimpleFieldClass fieldClass = mSession.With<SimpleFieldClass>().Get();
            Assert.NotNull(fieldClass.SomeField);
        }

        [Test]
        public void SimpleFieldClass_SomeOtherPropertyNotNull()
        {
            SimpleFieldClass fieldClass = mSession.With<SimpleFieldClass>().Get();
            Assert.NotNull(fieldClass.SomeOtherField);
        }


        [Test]
        public void DefaultPropertyClass_StringIsEmpty()
        {
            DefaultPropertyClass propertyClass = mSession.With<DefaultPropertyClass>().Get();
            Assert.AreEqual("", propertyClass.String);
        }

        [Test]
        public void DefaultPropertyClass_FloatEqualsZero()
        {
            DefaultPropertyClass propertyClass = mSession.With<DefaultPropertyClass>().Get();
            Assert.AreEqual(0, propertyClass.Float);
        }

        [Test]
        public void DefaultPropertyClass_IntegerEqualsZero()
        {
            DefaultPropertyClass propertyClass = mSession.With<DefaultPropertyClass>().Get();
            Assert.AreEqual(0, propertyClass.Integer);
        }

        [Test]
        public void DefaultPropertyClass_DateTimeIsMin()
        {
            DefaultPropertyClass propertyClass = mSession.With<DefaultPropertyClass>().Get();
            Assert.AreEqual(DateTime.MinValue, propertyClass.Date);
        }


        [Test]
        public void DefaultFieldClass_StringIsEmpty()
        {
            DefaultFieldClass propertyClass = mSession.With<DefaultFieldClass>().Get();
            Assert.AreEqual("", propertyClass.String);
        }

        [Test]
        public void DefaultFieldClass_FloatEqualsZero()
        {
            DefaultFieldClass propertyClass = mSession.With<DefaultFieldClass>().Get();
            Assert.AreEqual(0, propertyClass.Float);
        }

        [Test]
        public void DefaultFieldClass_IntegerEqualsZero()
        {
            DefaultFieldClass propertyClass = mSession.With<DefaultFieldClass>().Get();
            Assert.AreEqual(0, propertyClass.Integer);
        }

        [Test]
        public void DefaultFieldClass_DateTimeIsMin()
        {
            DefaultFieldClass propertyClass = mSession.With<DefaultFieldClass>().Get();
            Assert.AreEqual(DateTime.MinValue, propertyClass.Date);
        }


    }
}
