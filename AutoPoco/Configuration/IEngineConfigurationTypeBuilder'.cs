using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationTypeBuilder<TPoco>
    {
        /// <summary>
        /// Adds a specific rule for a member on the poco we're building rules for
        /// </summary>
        IEngineConfigurationTypeMemberBuilder<TPoco, TMember> Setup<TMember>(Expression<Func<TPoco, TMember>> expression);
    }
}
