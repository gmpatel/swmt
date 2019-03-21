using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Swmt.Concretes;
using Swmt.Interfaces;

namespace Swmt.Container
{
    public enum AppType
    {
        Default = 0,
        Console = 1,
        WebApp = 2,
        Mobile = 3
    }

    public static class ServiceProviderFactory
    {
        private static IDictionary<AppType, IServiceProvider> serviceProviders;
        
        static ServiceProviderFactory() {
            serviceProviders = new Dictionary<AppType, IServiceProvider>();
        }

        public static T GetComponent<T>(AppType type)
        {
            return GetProvider(type).GetService<T>();
        }

        public static IServiceProvider GetProvider(AppType type)
        {
            switch (type) {
                // case AppType.WebApp:
                // case AppType.Mobile:
                // Can return different services mapping for different type of apps!
                case AppType.Default:  
                case AppType.Console:
                default:
                {
                    if (!serviceProviders.ContainsKey(type)) 
                    {
                        var serviceCollection = new ServiceCollection()
                            .AddScoped<IAppService, ConsoleApplication>()
                            .AddScoped<IOutput, ConsoleOutput>()
                            .AddScoped(typeof(IDataSource<>), typeof(JsonDataSource<>));
            
                        serviceProviders[type] = serviceCollection.BuildServiceProvider();
                    }
                    break;
                }
            }

            return serviceProviders[type];
        }
    }
}
