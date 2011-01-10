using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    // Okay, not strictly a convention, perhaps should look at swapping the convention provider out for a proper container at some point
    // Still better than what was here ebfore
    public interface IEngineConfigurationProviderLoadingConvention : IConvention
    {
        void Apply(IEngineConfigurationProviderLoadingConventionContext context);
    }
}
