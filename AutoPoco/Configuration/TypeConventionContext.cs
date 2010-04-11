﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Util;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public class TypeConventionContext : ITypeConventionContext
    {
        private IEngineConfigurationType mType;

        public Type Target
        {
            get
            {
                return mType.RegisteredType;
            }
        }

        public void RegisterField(FieldInfo field)
        {
            var member = ReflectionHelper.GetMember(field);
            if (mType.GetRegisteredMember(member) == null)
            {
                mType.RegisterMember(member);
            }
        }

        public void RegisterProperty(PropertyInfo property)
        {
            var member = ReflectionHelper.GetMember(property);
            if (mType.GetRegisteredMember(member) == null)
            {
                mType.RegisterMember(ReflectionHelper.GetMember(property));
            }
        }

        public TypeConventionContext(IEngineConfigurationType type)
        {
            mType = type;
        }
    }
}
