using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Interfaces;
using Assessment_ChannelEngine.Common.Models;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Assessment_ChannelEngine.Core.Interface;
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
            var lines = new List<LineResult>();
            var orders = await GetAllOrdersInStatusInProgress();

            foreach (var orderLines in orders.Content.Select(_ => _.Lines))
                lines.AddRange(orderLines.ToList());

            lines = lines.GroupBy(_ => _.MerchantProductNo).Select(_ =>
                    new LineResult
                    {
                        MerchantProductNo = _.Key,
                        Quantity = _.Sum(x => x.Quantity)
                    })
                .OrderByDescending(_ => _.Quantity)
                .Take(5)
                .ToList();

            var productsResult = await ProductsResult(lines);

            var productsVmList = new List<ProductVm>();
            foreach (var line in lines)
            {
                var matchProduct = productsResult?.Content?
                                       .FirstOrDefault(_ => string.Equals(_.MerchantProductNo, line.MerchantProductNo))
                                   ?? throw new ApplicationException(
                                       $"Missing information about product {line.MerchantProductNo}");

                productsVmList.Add(new ProductVm
                {
                    Quantity = line.Quantity,
                    MerchantProductNo = line.MerchantProductNo,
                    Name = matchProduct.Name,
                    Ean = matchProduct.Ean
                });
            }

            return productsVmList
                .OrderByDescending(_ => _.Quantity)
                .ToList();
        }

        /// <inheritdoc />
        public async Task UpdateProductStockTo25(string merchantProductNo)
        {
            if (string.IsNullOrEmpty(merchantProductNo))
                throw new ArgumentNullException(nameof(merchantProductNo));

            var product = new LineResult
            {
                MerchantProductNo = merchantProductNo
            };
            //Call to have up to date product
            var productsResult = await ProductsResult(new List<LineResult> {product});

            var firstProduct = productsResult?.Content?.FirstOrDefault()
                               ?? throw new ApplicationException("Product not exist");

            firstProduct.Stock = 25;

            await _restClient.PostAsync<UpdateProductResult, UpdateProductRequest>
            ("products",
                new UpdateProductRequest
                {
                    Products = new List<ProductResult> {firstProduct}
                });
        }

        private async Task<ProductsResult> ProductsResult(List<LineResult> lines)
        {
            var query = BuildProductsApiQuery(lines);
            return await _restClient.GetAsync<ProductsResult>($"products?{query}");
        }

        private string BuildProductsApiQuery(List<LineResult> lines)
        {
            var query = string.Empty;
            lines.ForEach(_ => query += $"merchantProductNoList={_.MerchantProductNo}&");
            query.Remove(query.Length - 1); // delete last &
            return query;
        }
    }
}