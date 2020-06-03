using Assessment_ChannelEngine.Console.Interface;
using Assessment_ChannelEngine.Console.Logger;
using Assessment_ChannelEngine.Console.Proxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Assessment_ChannelEngine.Console
{
    public class RegisterContainers
    {
        private readonly ServiceProvider _serviceProvider;
    
        public RegisterContainers()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(configure => 
                    configure
                        .ClearProviders()
                        .AddProvider(new LoggerProvider()))
                .AddSingleton<IConsoleHandler, ConsoleHandler>()
                .AddSingleton<IOrdersServiceProxy, OrdersServiceProxy>()
                .BuildServiceProvider();
        }

        public T GetService<T>() => _serviceProvider.GetService<T>();
    }
}