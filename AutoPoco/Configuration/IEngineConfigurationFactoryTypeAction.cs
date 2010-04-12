using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationFactoryTypeAction
    {
        void Apply(IEngineConfigurationType type);
    }
}
