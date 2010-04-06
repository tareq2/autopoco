using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using AutoPoco.Configuration;

namespace AutoPoco.Util
{
    public static class ReflectionHelper
    {
        public static EngineTypeMember GetMember<TPoco, TReturn>(Expression<Func<TPoco, TReturn>> expression)
        {
            var member = GetMemberInfo(expression.Body);
            return GetMember(member);
        }

        public static EngineTypeMember GetMember<TPoco>(Expression<Func<TPoco, object>> expression)
        {
            var member = GetMemberInfo(expression.Body);
            return GetMember(member);
        }

        public static PropertyInfo GetProperty<TPoco>(Expression<Func<TPoco, object>> expression)
        {
            var member = GetMemberInfo(expression.Body);
            return member.ReflectedType.GetProperty(member.Name);
        }

        public static FieldInfo GetField<TPoco>(Expression<Func<TPoco, object>> expression)
        {
            var member = GetMemberInfo(expression.Body);
            return member.ReflectedType.GetField(member.Name);
        }

        private static MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Expression not supported", "expression");
            }

            return memberExpression.Member;              
        }


        public static EngineTypeMember GetMember(MemberInfo info)
        {
            if (info is PropertyInfo)
            {
                return new EngineTypePropertyMember(info as PropertyInfo);
            }
            if (info is MethodInfo)
            {
                return new EngineTypeMethodMember(info as MethodInfo);
            }
            if (info is FieldInfo)
            {
                return new EngineTypeFieldMember(info as FieldInfo);
            }
            throw new ArgumentException("Unsupported member type", "info");
        }
    }
}
