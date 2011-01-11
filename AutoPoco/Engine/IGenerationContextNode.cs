using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public interface IGenerationContextNode
    {
        /// <summary>
        /// Gets the details for which why context has been invoked
        /// </summary>
        IGenerationContextNode Site { get; }
       
        /// <summary>
        /// Gets the member for which this node was created
        /// </summary>
        EngineTypeMember TargetMember { get; }

        /// <summary>
        /// Gets the object for which this node was created
        /// </summary>
        Object TargetObject { get; }
    }
}
