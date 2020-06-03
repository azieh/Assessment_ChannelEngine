using System.Collections.Generic;

namespace Assessment_ChannelEngine.Common.Models.ChannelEngine
{
    public class UpdateProductRequest
    {
        public List<Product> Products { get; set; } = new List<Product>();
    }
}