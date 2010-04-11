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
            context.SetValue(DateTime.MinValue);
        }

        public void Apply(ITypeFieldConventionContext context)
        {
            context.SetValue(DateTime.MinValue);
        }

        public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
        {
            requirements.Type(x => x == typeof(DateTime));
        }
    }
}
