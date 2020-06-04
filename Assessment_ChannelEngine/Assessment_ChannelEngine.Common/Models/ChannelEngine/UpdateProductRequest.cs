using System.Collections.Generic;

namespace Assessment_ChannelEngine.Common.Models.ChannelEngine
{
    public class UpdateProductRequest
    {
        public List<ProductResult> Products { get; set; } = new List<ProductResult>();
    }
}