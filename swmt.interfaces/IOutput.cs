using System;
using System.Collections.Generic;

namespace Swmt.Interfaces 
{
    public interface IOutput
    {
        void WriteLine(string format, params object[] arg);
    }
}