using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IDatasource
    {
        Object Next(IGenerationSession session);
    }

    public interface IDatasource<T> : IDatasource
    {
        
    }
}
