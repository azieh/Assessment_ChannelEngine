using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assessment_ChannelEngine.Core.Interface
{
    public interface IHttpClientWrapper : IDisposable
    {
        /// <summary>Gets the stream asynchronous.</summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public Task<Stream> GetStreamAsync(string url);
        /// <summary>Posts the asynchronous.</summary>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}