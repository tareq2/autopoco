using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public class EngineTypeMethodMember : EngineTypeMember
    {
        private MethodInfo mMethodInfo;

        public override string Name
        {
            get { return mMethodInfo.Name; }
        }

        public override bool IsMethod
        {
            get { return true; }
        }

        public override bool IsField
        {
            get { return false; }
        }

        public override bool IsProperty
        {
            get { return false; }
        }

        public MethodInfo MethodInfo
        {
            get { return mMethodInfo; }
        }

        public EngineTypeMethodMember(MethodInfo methodInfo)
        {
            mMethodInfo = methodInfo;
        }

        public override bool Equals(object obj)
        {
            var otherMember = obj as EngineTypeMethodMember;
            if (otherMember != null)
            {
                // Yes, this is going to come back and bite me in the ass too
                // As it's purely structural and doesn't obey hierarchy etc
                return (otherMember.MethodInfo.Name == this.MethodInfo.Name) && ArgumentsAreEqual(otherMember.MethodInfo, this.MethodInfo);
                    
            }
            return false;
        }

        private bool ArgumentsAreEqual(MethodInfo one, MethodInfo two)
        {
            var paramOne = one.GetParameters();
            var paramTwo = two.GetParameters();

            if (paramTwo.Length != paramOne.Length) return false;

            for (int x = 0; x < paramOne.Length; x++)
            {
                if( paramOne[x] != paramTwo[x]) { return false;}
            }
            
            return true;
        }

        public override int GetHashCode()
        {
            return mMethodInfo.GetHashCode();
        }
    }
}
