using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IGenerationSessionFactory
    {
        /// <summary>
        /// Creates a session from this configured factory
        /// </summary>
        /// <returns></returns>
        IGenerationSession CreateSession();
    }
}
