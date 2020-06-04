using System.Collections.Generic;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Interfaces;
using Assessment_ChannelEngine.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_ChannelEngine.Web.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public ProductsController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("Top5")]
        public async Task<IEnumerable<ProductVm>> GetTop5()
        {
            return await _ordersService.GetTop5ProductSold();
        }

        [HttpPost]
        [Route("UpdateStockTo25")]
        public async Task PostUpdateStockTo25(string merchantProductNo)
        {
            await _ordersService.UpdateProductStockTo25(merchantProductNo);
        }
    }
}