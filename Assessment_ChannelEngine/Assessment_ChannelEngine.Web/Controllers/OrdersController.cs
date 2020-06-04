using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Interfaces;
using Assessment_ChannelEngine.Common.Models;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_ChannelEngine.Web.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("Top5")]
        public async Task<IEnumerable<ProductVm>> GetTop5()
        {
            return await _ordersService.GetTop5ProductSold();
        }

        [HttpGet]
        [Route("InProcess")]
        public async Task<IEnumerable<OrderResult>> GetAllOrdersInProcess()
        {
            var result = await _ordersService.GetAllOrdersInStatusInProgress();
            return result.Content.ToList();
        }

        [HttpPost]
        [Route("UpdateStockTo25")]
        public async Task PostUpdateStockTo25(string merchantProductNo)
        {
            await _ordersService.UpdateProductStockTo25(merchantProductNo);
        }
    }
}