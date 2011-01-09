using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using AutoPoco.Util;

namespace AutoPoco.Configuration
{
    public class EngineTypePropertyMember : EngineTypeMember
    {
        private PropertyInfo mPropertyInfo;

        public override string Name
        {
            get { return mPropertyInfo.Name; }
        }

        public override bool IsMethod
        {
            get { return false; }
        }

        public override bool IsField
        {
            get { return false; }
        }

        public override bool IsProperty
        {
            get { return true; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return mPropertyInfo; }
        }

        public EngineTypePropertyMember(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) { throw new ArgumentNullException("propertyInfo"); }
            mPropertyInfo = propertyInfo;
        }
        
        public override bool Equals(object obj)
        {
            var otherMember = obj as EngineTypePropertyMember;
            if (otherMember != null)
            {
                var propertyOne = GetSourceProperty(otherMember.PropertyInfo);
                var propertyTwo = GetSourceProperty(this.PropertyInfo);

                if (propertyOne == null) { throw new InvalidOperationException("AGH1"); }
                if (propertyTwo == null) { throw new InvalidOperationException("AGH2"); }

                return propertyOne.MetadataToken == propertyTwo.MetadataToken &&
                    propertyOne.Module == propertyTwo.Module;
                   
            }
            return false;
        }

        private PropertyInfo GetRootProperty(PropertyInfo pi)
        {
            if ((pi.GetGetMethod().Attributes & MethodAttributes.Virtual) != MethodAttributes.Virtual) { return pi; }

            var type = pi.DeclaringType;

            while (true)
            {
                type = type.BaseType;

                if (type == null)
                {
                    return pi;
                }

                var flags = BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance |
                            BindingFlags.Public | BindingFlags.Static;

                var inheritedProperty = type.GetProperty(pi.Name, flags);

                if (inheritedProperty == null)
                {
                    return pi;
                }

                pi = inheritedProperty;
            }
        }

        private PropertyInfo GetSourceProperty(PropertyInfo pi)
        {
            var inherited = this.GetRootProperty(pi);
            if (inherited != pi)
            {
                pi = inherited;
            }

            var implemented = this.GetImplementedProperty(pi);
            if (implemented != pi)
            {
                return implemented;
            }

            return pi;
        }

        private PropertyInfo GetImplementedProperty(PropertyInfo pi)
        {
            var type = pi.DeclaringType;
            var interfaces = type.GetInterfaces();

            for(int interfaceIndex = 0; interfaceIndex < interfaces.Length; interfaceIndex++)
            {
                var iface = interfaces[interfaceIndex];
                var interfaceMethods = type.GetInterfaceMap(iface).TargetMethods;

                MethodInfo matchingMethod = null;
                for (int x = 0; x < interfaceMethods.Length; x++)
                {
                    if (pi.GetGetMethod().LooseCompare(interfaceMethods[x]) || pi.GetSetMethod().LooseCompare(interfaceMethods[x]))
                    {
                        matchingMethod = type.GetInterfaceMap(iface).InterfaceMethods[x];
                        break; 
                    }
                }
                if (matchingMethod == null) continue;

                var interfacePi = from i in interfaces
                                  from property in i.GetProperties()
                                  where property.GetGetMethod().LooseCompare(matchingMethod) || property.GetSetMethod().LooseCompare(matchingMethod)
                                  select property;

                return interfacePi.First();
            }
            
            return pi;
        } 

        public override int GetHashCode()
        {
            return PropertyInfo.GetHashCode();
        }
    }
}
