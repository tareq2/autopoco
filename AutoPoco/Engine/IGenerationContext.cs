using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public interface IGenerationContext
    {
        /// <summary>
        /// Gets a session from which new objects can be requested and created from
        /// </summary>
        IGenerationSession Session { get; }

        /// <summary>
        /// Gets the context for which this call is being made
        /// </summary>
        IGenerationContextNode Site { get; }
    }
}
