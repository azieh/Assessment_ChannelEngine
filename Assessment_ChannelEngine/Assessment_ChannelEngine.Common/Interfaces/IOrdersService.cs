using System.Collections.Generic;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Models;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;

namespace Assessment_ChannelEngine.Common.Interfaces
{
    public interface IOrdersService
    {
        /// <summary>Gets all orders in status in progress.</summary>
        /// <returns>OrdersResult</returns>
        public Task<OrdersResult> GetAllOrdersInStatusInProgress();

        /// <summary>Gets the top5 product sold.</summary>
        /// <returns>List of ProductVm</returns>
        public Task<ICollection<ProductVm>> GetTop5ProductSold();

        /// <summary>Updates the product stock to25.</summary>
        /// <param name="merchantProductNo">The merchant product no.</param>
        /// <returns></returns>
        public Task UpdateProductStockTo25(string merchantProductNo);
    }
}