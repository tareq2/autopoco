using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;

namespace AutoPoco.Conventions
{
    public class ReferenceMemberConvention : ITypeFieldConvention, ITypePropertyConvention
    {
        public void Apply(ITypeFieldConventionContext context)
        {
            if (context.Configuration.GetRegisteredTypes()
                .Where(x => x.RegisteredType == context.Member.FieldInfo.FieldType).Count() > 0)
            {
                context.SetSource(typeof(AutoSource<>).MakeGenericType(context.Member.FieldInfo.FieldType));               
            }
        }

       public void Apply(ITypePropertyConventionContext context)
        {
            if (context.Configuration.GetRegisteredTypes()            
                .Where(x => x.RegisteredType == context.Member.PropertyInfo.PropertyType).Count() > 0)
            {
                context.SetSource(typeof(AutoSource<>).MakeGenericType(context.Member.PropertyInfo.PropertyType));
            }
        }

    }
}
