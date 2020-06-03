using System;
using Assessment_ChannelEngine.Console.Interface;
using Microsoft.Extensions.Logging;

namespace Assessment_ChannelEngine.Console.Proxy
{
    public class OrdersServiceProxy : IOrdersServiceProxy
    {
        private readonly ILogger<OrdersServiceProxy> _logger;
        public OrdersServiceProxy(ILogger<OrdersServiceProxy> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <inheritdoc />
        public void SetQuantityTo25()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void GetTop5SoldProductByQuantity()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void FetchAllIN_PROGRESS()
        {
            throw new System.NotImplementedException();
        }
    }
}