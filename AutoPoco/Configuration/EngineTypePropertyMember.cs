using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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
                var propertyOne = GetRootPropertyDefinition(otherMember.PropertyInfo);
                var propertyTwo = GetRootPropertyDefinition(this.PropertyInfo);
                
             //   return pro


                return propertyOne == propertyTwo;

                /*
                
                // Yes, this is simplistic and going to bite me in the ass soon enough
                return
                    (otherMember.PropertyInfo.Name == this.PropertyInfo.Name) &&
                    (otherMember.PropertyInfo.PropertyType == this.PropertyInfo.PropertyType); */
            }
            return false;
        }

        private object GetRootPropertyDefinition(PropertyInfo propertyInfo)
        {
            return propertyInfo.DeclaringType.GetProperty(
                propertyInfo.Name,
                BindingFlags.Default,
                null,
                propertyInfo.PropertyType,
                propertyInfo.GetIndexParameters().Select(x => x.ParameterType).ToArray(),
                null);
        }

        public override int GetHashCode()
        {
            return PropertyInfo.GetHashCode();
        }
    }
}
