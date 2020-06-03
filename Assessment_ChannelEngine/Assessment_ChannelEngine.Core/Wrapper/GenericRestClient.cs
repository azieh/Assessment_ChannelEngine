using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Services.Interfaces;

namespace Assessment_ChannelEngine.Core.Wrapper
{
    public class GenericRestClient : IGenericRestClient, IDisposable
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private string _apiKey;
        private string _baseUrlAddress;

        internal bool WasConfigCalled;

        public GenericRestClient(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _httpClientWrapper?.Dispose();
        }

        public bool IsSetupCalled => WasConfigCalled;

        /// <inheritdoc />
        public void Configure(string baseUrlAddress, string apiKey)
        {
            _baseUrlAddress = baseUrlAddress ?? throw new ArgumentNullException(nameof(baseUrlAddress));
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            WasConfigCalled = true;
        }

        /// <inheritdoc />
        public async Task<TResult> GetAsync<TResult>(string apiUrl) where TResult : class
        {
            EnsureRequiredArgumentAndAction(apiUrl);

            var httpResponse = await _httpClientWrapper.GetStreamAsync(GetCompleteApiUrl(apiUrl));

            return await JsonSerializer.DeserializeAsync<TResult>(httpResponse);
        }

        public async Task<TResult> PostAsync<TResult, TBody>(string apiUrl, TBody body)
            where TResult : class
            where TBody : class
        {
            EnsureRequiredArgumentsAndAction(apiUrl, body);

            var dataAsString = JsonSerializer.Serialize(body);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpResponse = await _httpClientWrapper.PostAsync(GetCompleteApiUrl(apiUrl), content);
            var stream = await ReadAndReturnResponseStream(httpResponse);
            return await JsonSerializer.DeserializeAsync<TResult>(stream);
        }

        private static async Task<Stream> ReadAndReturnResponseStream(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStreamAsync();
        }

        private void EnsureRequiredArgumentAndAction(string apiUrl)
        {
            if (string.IsNullOrWhiteSpace(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl));
            if (!IsSetupCalled)
                throw new InvalidOperationException("Call [Configure] method first");
        }

        private void EnsureRequiredArgumentsAndAction<TBody>(string apiUrl, TBody body)
        {
            if (null == body)
                throw new ArgumentNullException(nameof(body));
            EnsureRequiredArgumentAndAction(apiUrl);
        }

        private string GetCompleteApiUrl(string apiUrl)
        {
            return $"{_baseUrlAddress}{apiUrl}&apikey={_apiKey}";
        }
    }
}