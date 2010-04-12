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

        internal EngineTypePropertyMember(PropertyInfo propertyInfo)
        {
            mPropertyInfo = propertyInfo;
        }

        public override bool Equals(object obj)
        {
            var otherMember = obj as EngineTypePropertyMember;
            if (otherMember != null)
            {
                return otherMember.PropertyInfo == this.PropertyInfo;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return PropertyInfo.GetHashCode();
        }
    }
}
