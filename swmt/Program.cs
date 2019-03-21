using System;
using Microsoft.Extensions.DependencyInjection;
using Swmt.Container;
using Swmt.Interfaces;

namespace Swmt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var app = ServiceProviderFactory.GetComponent<IAppService>(AppType.Console))
            {
                app.Run();
            }
        }
    }
}