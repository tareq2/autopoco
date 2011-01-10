using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Conventions
{
    public class AdhocTypeRegistrationConvention : DefaultTypeRegistrationConvention
    {
        public override void ApplyTypeMemberConfiguration(Configuration.ITypeRegistrationConventionContext context)
        {
           // Nada
        }

        public override void RegisterTypeMembersFromConfiguration(Configuration.ITypeRegistrationConventionContext context)
        {
            // Nada
        }
    }
}
