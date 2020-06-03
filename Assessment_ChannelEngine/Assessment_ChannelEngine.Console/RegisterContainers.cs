using System;
using Assessment_ChannelEngine.Console.Interface;
using Assessment_ChannelEngine.Console.Logger;
using Assessment_ChannelEngine.Console.Proxy;
using Assessment_ChannelEngine.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Assessment_ChannelEngine.Console
{
    public class RegisterContainers
    {
        private readonly IServiceProvider _serviceProvider;

        public RegisterContainers()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddLogging(configure =>
                    configure
                        .ClearProviders()
                        .AddProvider(new LoggerProvider()))
                .AddSingleton(config)
                .AddSingleton<IConsoleHandler, ConsoleHandler>()
                .AddSingleton<IOrdersServiceProxy, OrdersServiceProxy>();
            ServicesContainers.ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}