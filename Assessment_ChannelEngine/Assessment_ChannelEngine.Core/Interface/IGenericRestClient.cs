using System.Threading.Tasks;

namespace Assessment_ChannelEngine.Services.Interfaces
{
    public interface IGenericRestClient
    {
        /// <summary>
        ///     Returns true if Configuration was called first
        /// </summary>
        bool IsSetupCalled { get; }
        void Configure(string baseUrlAddress, string apiKey);

        /// <summary>
        ///     Perform GET & returns multiple/all items from resource
        ///     
        /// </summary>
        /// <typeparam name="TResult">expected results type</typeparam>
        /// <param name="apiUrl">
        ///     Added to the base address to make the full url of the GET method, e.g. "products?page=1" to get page 1 of the products
        /// </param>
        /// <returns>The items requested</returns>
        /// <remarks>GET = read call</remarks>
        Task<TResult> GetAsync<TResult>(string apiUrl) where TResult : class;

        /// <summary>
        ///     Perform POST & returns response item(s)
        /// </summary>
        /// <typeparam name="TResult">expected result type</typeparam>
        /// <typeparam name="TBody">request body object</typeparam>
        /// <param name="apiUrl">
        ///     Added to the base address to make the full url of the POST method, e.g. "products" to add products
        /// </param>
        /// <param name="body">
        ///     The request body object with proper data
        /// </param>
        /// <returns>The POST response item</returns>
        /// <remarks>POST = create call</remarks>
        Task<TResult> PostAsync<TResult, TBody>(string apiUrl, TBody body)
            where TResult : class
            where TBody : class;
    }
}