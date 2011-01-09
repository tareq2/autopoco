using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineTypePropertyTests
    {
        [Test]
        public void Same_Property_Requested_From_Base_And_Derived_Types_Is_Considered_Equal()
        {
            EngineTypePropertyMember baseProperty = new EngineTypePropertyMember(typeof(BaseClass).GetProperty("SealedProperty"));
            EngineTypePropertyMember derivedProperty = new EngineTypePropertyMember(typeof(DerivedClass).GetProperty("SealedProperty"));

            Assert.True(baseProperty == derivedProperty);
        }

        [Test]
        public void Same_Property_Requested_From_Interface_And_Implementing_Type_Is_Considered_Equal()
        {
            EngineTypePropertyMember interfaceProperty = new EngineTypePropertyMember(typeof(IFoo).GetProperty("InterfaceProperty"));
            EngineTypePropertyMember implementedProperty = new EngineTypePropertyMember(typeof(BaseClass).GetProperty("InterfaceProperty"));

            Assert.True(interfaceProperty == implementedProperty);
        }

        
        [Test]
        public void Same_Property_Requested_From_Interface_And_DerivedType_From_Implementing_Type_Is_Considered_Equal()
        {
            EngineTypePropertyMember interfaceProperty = new EngineTypePropertyMember(typeof(IFoo).GetProperty("InterfaceProperty"));
            EngineTypePropertyMember deriveddProperty = new EngineTypePropertyMember(typeof(DerivedClass).GetProperty("InterfaceProperty"));

            Assert.True(interfaceProperty == deriveddProperty);
        }

        [Test]
        public void Overridden_Property_Requested_From_Base_And_Derived_Type_Is_ConsideredEqual()
        {
            EngineTypePropertyMember baseProperty = new EngineTypePropertyMember(typeof(BaseClass).GetProperty("VirtualProperty"));
            EngineTypePropertyMember overriddenProperty = new EngineTypePropertyMember(typeof(DerivedClass).GetProperty("VirtualProperty"));

            Assert.True(baseProperty == overriddenProperty);
        }

        [Test]
        public void New_Property_Requested_From_Derived_Type_Is_Not_Considered_To_Hidden_Property()
        {
            EngineTypePropertyMember baseProperty = new EngineTypePropertyMember(typeof(BaseClass).GetProperty("AnotherSealedProperty"));
            EngineTypePropertyMember overriddenProperty = new EngineTypePropertyMember(typeof(DerivedClass).GetProperty("AnotherSealedProperty"));

            Assert.False(baseProperty == overriddenProperty);
        }

        public interface IFoo
        {
            string InterfaceProperty { get; set; }
        }

        public class BaseClass
        {
            public string SealedProperty { get; set; }
            public string AnotherSealedProperty { get; set; }
            public virtual string VirtualProperty  { get; set; }
            public string InterfaceProperty { get; set; }
        }

        public class DerivedClass : BaseClass
        {
            public override string VirtualProperty { get; set; }
            public new string AnotherSealedProperty { get; set; }
        }
    }
}
