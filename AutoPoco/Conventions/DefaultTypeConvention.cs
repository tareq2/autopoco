using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Conventions
{
    public class DefaultTypeConvention : ITypeConvention
    {
        public void Apply(ITypeConventionContext context)
        {
            // Register every public property on this type
            foreach(var property in context.Target.GetProperties( System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                context.RegisterProperty(property);
            }

            // Register every public field on this type
            foreach (var field in context.Target.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                context.RegisterField(field);
            }                
        }

        public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
        {
            throw new NotImplementedException();
        }
    }
}
