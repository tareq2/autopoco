using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Conventions;
using AutoPoco.Configuration;
using Moq;
using System.Reflection;

namespace AutoPoco.Tests.Unit.Conventions
{
    [TestFixture]
    public class DefaultTypeConventionTests
    {
        DefaultTypeConvention mConvention;
        Mock<ITypeConventionContext> mTypeConventionContext;

        [SetUp]
        public void SetupObjects()
        {
            mConvention = new DefaultTypeConvention();
            mTypeConventionContext = new Mock<ITypeConventionContext>();         
        }

        [Test]
        public void Apply_IgnoresBaseProperties()
        {
            int count = 0;
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(Class));   
            mTypeConventionContext.Setup(x => x.RegisterProperty(It.IsAny<PropertyInfo>()))
                .Callback(() =>
                {
                    count++;
                });

            mConvention.Apply(mTypeConventionContext.Object);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Apply_IgnoresBaseFields()
        {
            int count = 0;
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(Class));   
            mTypeConventionContext.Setup(x => x.RegisterField(It.IsAny<FieldInfo>()))
                .Callback(() =>
                {
                    count++;
                });

            mConvention.Apply(mTypeConventionContext.Object);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Apply_HandlesNestedInterfaces()
        {
            int count = 0;
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(ITestInterface));
            mTypeConventionContext.Setup(x => x.RegisterProperty(It.IsAny<PropertyInfo>()))
                .Callback(() =>
                {
                    count++;
                });

            mConvention.Apply(mTypeConventionContext.Object);

            Assert.AreEqual(1, count);
        }

        public class BaseClass : IBaseTestInteface
        {
            public string BaseProperty
            {
                get;
                set;
            }

            public string BaseField;

            public string BaseInterfaceProperty
            {
                get;
                set;
            }
        }

        public class Class : BaseClass, ITestInterface
        {
            public string TopProperty
            {
                get;
                set;
            }

            public string InterfaceProperty
            {
                get;
                set;
            }

            public string TopField;
        }

        public interface IBaseTestInteface
        {
            string BaseInterfaceProperty
            {
                get;
                set;
            }
        }


        public interface ITestInterface : IBaseTestInteface
        {
            string InterfaceProperty
            {
                get;
                set;
            }
        }
    }
}
