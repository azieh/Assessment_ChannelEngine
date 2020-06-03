using Assessment_ChannelEngine.Core;
using Assessment_ChannelEngine.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment_ChannelEngine.Services
{
    public static class ServicesContainers
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            CoreContainers.ConfigureServices(services);
            services.AddScoped<IOrdersService, OrdersService>();
        }
    }
}