using System;
using Assessment_ChannelEngine.Common.Interfaces;
using Assessment_ChannelEngine.Console.Interface;
using Microsoft.Extensions.Logging;

namespace Assessment_ChannelEngine.Console.Proxy
{
    public class OrdersServiceProxy : IOrdersServiceProxy
    {
        private readonly ILogger<OrdersServiceProxy> _logger;
        private readonly IOrdersService _ordersService;

        public OrdersServiceProxy(ILogger<OrdersServiceProxy> logger, IOrdersService ordersService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ordersService = ordersService ?? throw new ArgumentNullException(nameof(ordersService));
        }

        /// <inheritdoc />
        public void SetQuantityTo25()
        {
            _logger.LogInformation("Please type MerchantProductNo");

            var command = System.Console.ReadLine();
            if (string.IsNullOrEmpty(command))
            {
                _logger.LogWarning("Input is not valid");
                return;
            }

            try
            {
                var updateTask = _ordersService.UpdateProductStockTo25(command);
                updateTask.Wait();
                _logger.LogInformation("Success");
                _logger.LogInformation(string.Empty);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
            }
        }

        /// <inheritdoc />
        public void GetTop5SoldProductByQuantity()
        {
            var getTask = _ordersService.GetTop5ProductSold();
            var result = getTask.Result;
            foreach (var product in result)
                _logger.LogInformation($"Product name: {product.Name} | Ean: {product.Ean} | Quantity: {product.Quantity}");
            
            _logger.LogInformation(string.Empty);
        }

        /// <inheritdoc />
        public void FetchAllIN_PROGRESS()
        {
            var getTask = _ordersService.GetAllOrdersInStatusInProgress();
            var result = getTask.Result;

            foreach (var order in result.Content)
            {
                _logger.LogInformation($"Order Id: {order.Id}");

                foreach (var line in order.Lines)
                    _logger.LogInformation($"MerchantProductNo: {line.MerchantProductNo} | Quantity: {line.Quantity}");

                _logger.LogInformation(string.Empty);
            }
        }
    }
}