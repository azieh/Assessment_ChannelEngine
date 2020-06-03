using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assessment_ChannelEngine.Services.Interfaces
{
    public interface IHttpClientWrapper : IDisposable
    {
        public Task<Stream> GetStreamAsync(string url);
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}