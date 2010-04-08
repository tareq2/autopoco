﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using AutoPoco.Configuration;
using System.Reflection;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class TypeConventionContextTests
    {
        private Mock<IEngineConfigurationType> mTypeMock;
        private TypeConventionContext mContext;

        [SetUp]
        public void Setup()
        {
            mTypeMock = new Mock<IEngineConfigurationType>();
            mContext = new TypeConventionContext(mTypeMock.Object);
        }

        [Test]
        public void RegisterField_TypeMemberRegistered()
        {
            FieldInfo field = typeof(TestClass).GetField("Field");
            mContext.RegisterField(field);
            mTypeMock.Verify(x => x.RegisterMember(It.Is<EngineTypeMember>(y => y.Name == field.Name)), Times.Once());
        }

        [Test]
        public void RegisterProperty_TypePropertyRegistered()
        {
            PropertyInfo property = typeof(TestClass).GetProperty("Property");
            mContext.RegisterProperty(property);
            mTypeMock.Verify(x => x.RegisterMember(It.Is<EngineTypeMember>(y => y.Name == property.Name)), Times.Once());
        }

        [Test]
        public void Target_ReturnsConfigurationType()
        {
            mTypeMock.SetupGet(x => x.RegisteredType).Returns(typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), mContext.Target);
        }

        public class TestClass
        {
            public string Field;
            public string Property
            {
                get;
                set;
            }
        }
    }
}