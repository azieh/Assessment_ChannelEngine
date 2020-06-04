namespace Assessment_ChannelEngine.Common.Models.ChannelEngine
{
    public class ProductsResult
    {
        public Product[] Content { get; set; }
        public bool Success { get; set; }
    }

    public class Product
    {
        public string MerchantProductNo { get; set; }
        public string Name { get; set; }
        public string Ean { get; set; }
        public int Stock { get; set; }
    }
}