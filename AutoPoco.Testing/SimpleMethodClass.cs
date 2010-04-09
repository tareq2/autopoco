using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleMethodClass
    {
        public string Value
        {
            get;
            set;
        }

        public void SetSomething(String value)
        {
            this.Value = value;
        }

        public string ReturnSomething()
        {
            return string.Empty;
        }
    }
}
