using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IGenerationSession
    {
        IObjectGenerator<T> With<T>();
    }
}
