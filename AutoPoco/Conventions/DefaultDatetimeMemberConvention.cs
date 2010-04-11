using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Conventions
{
    public class DefaultDatetimeMemberConvention : ITypeFieldConvention, ITypePropertyConvention
    {
        public void Apply(ITypePropertyConventionContext context)
        {
            if (context.Member.PropertyInfo.PropertyType == typeof(DateTime))
            {
                context.SetValue(DateTime.MinValue);
            }
        }

        public void Apply(ITypeFieldConventionContext context)
        {
            if (context.Member.FieldInfo.FieldType == typeof(DateTime))
            {
                context.SetValue(DateTime.MinValue);
            }
        }

        public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
        {
            throw new NotImplementedException();
        }
    }
}
