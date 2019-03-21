using System;
using System.Collections.Generic;
using Swmt.Interfaces;

namespace Swmt.Concretes 
{
    public class ConsoleOutput : IOutput
    {
        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}