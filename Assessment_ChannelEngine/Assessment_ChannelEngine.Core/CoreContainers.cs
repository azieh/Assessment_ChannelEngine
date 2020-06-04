using Assessment_ChannelEngine.Core.Interface;
using Assessment_ChannelEngine.Core.Wrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Assessment_ChannelEngine.Core
{
    public static class CoreContainers
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
            services.AddScoped<IGenericRestClient, GenericRestClient>();
        }
    }
}