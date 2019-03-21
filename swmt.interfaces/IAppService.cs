using System;
using System.Collections.Generic;

namespace Swmt.Interfaces 
{
    public interface IAppService : IDisposable
    {
        void Run();
    }
}