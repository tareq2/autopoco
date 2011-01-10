﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class ApplyTypeConventionsAction : TypeRegistrationAction
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventionProvider;

        public ApplyTypeConventionsAction(IEngineConfiguration configuration, IEngineConventionProvider conventions)
        {
            mConfiguration = configuration;
            mConventionProvider = conventions;
        }

        public override void Apply(IEngineConfigurationType type)
        {
            mConventionProvider.Find<ITypeConvention>()
                .Select(t => (ITypeConvention)Activator.CreateInstance(t))
                .ToList()
                .ForEach(x =>
                {
                     x.Apply(new TypeConventionContext(type));
                });
        }
    }
}
