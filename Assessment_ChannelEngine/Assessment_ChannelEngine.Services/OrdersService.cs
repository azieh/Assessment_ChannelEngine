using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Interfaces;
using Assessment_ChannelEngine.Common.Models;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Assessment_ChannelEngine.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Assessment_ChannelEngine.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRestClient _restClient;

        public OrdersService(IConfiguration configuration, IGenericRestClient restClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
            _restClient.Configure(GetApiUrl, GetApiKey);
        }

        private string GetApiUrl => _configuration["ChannelEngineApi:BaseUrl"];
        private string GetApiKey => _configuration["ChannelEngineApi:ApiKey"];

        /// <inheritdoc />
        public async Task<OrdersResult> GetAllOrdersInStatusInProgress()
        {
            return await _restClient.GetAsync<OrdersResult>("orders?statuses=IN_PROGRESS");
        }


        /// <inheritdoc />
        public async Task<ICollection<ProductVm>> GetTop5ProductSold()
        {
            var lines = new List<Line>();
            var orders = await GetAllOrdersInStatusInProgress();

            foreach (var orderLines in orders.Content.Select(_ => _.Lines))
                lines.AddRange(orderLines.ToList());

            lines = lines.GroupBy(_ => _.MerchantProductNo).Select(_ =>
                    new Line
                    {
                        MerchantProductNo = _.Key,
                        Quantity = _.Sum(x => x.Quantity)
                    })
                .OrderByDescending(_ => _.Quantity)
                .Take(5)
                .ToList();

            var query = BuildProductsApiQuery(lines);

            var productsResult = await _restClient.GetAsync<ProductsResult>($"products?{query}");

            return productsResult.Content.Select(_ => new ProductVm
                {
                    Name = _.Name,
                    Ean = _.Ean,
                    MerchantProductNo = _.MerchantProductNo,
                    Quantity = lines
                        .First(line => string.Equals(line.MerchantProductNo, _.MerchantProductNo))
                        .Quantity
                })
                .OrderByDescending(_ => _.Quantity)
                .ToList();
        }

        /// <inheritdoc />
        public async Task UpdateProductStockTo25(string merchantProductNo)
        {
            var product = new Product
            {
                MerchantProductNo = merchantProductNo,
                Stock = 25
            };
            var requestModel = new UpdateProductRequest();
            requestModel.Products.Add(product);

            await _restClient.PostAsync<UpdateProductRequest, UpdateProductRequest>("products", requestModel);
        }

        private string BuildProductsApiQuery(List<Line> lines)
        {
            var query = string.Empty;
            lines.ForEach(_ => query += $"merchantProductNoList={_.MerchantProductNo}&");
            query.Remove(query.Length - 1); // delete last &
            return query;
        }
    }
}