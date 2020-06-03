using System.Collections.Generic;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Models;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;

namespace Assessment_ChannelEngine.Common.Interfaces
{
    public interface IOrdersService
    {
        public Task<OrdersResult> GetAllOrdersInStatusInProgress();

        public Task<ICollection<ProductVm>> GetTop5ProductSold();

        public Task UpdateProductStockTo25(string merchantProductNo);
    }
}