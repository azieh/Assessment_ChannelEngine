namespace Assessment_ChannelEngine.Common.Models
{
    public class ProductVm
    {
        public string Name { get; set; }
        public string MerchantProductNo { get; set; }
        public string Ean { get; set; }
        public int Quantity { get; set; }
        public bool IsUpdated { get; set; }
    }
}