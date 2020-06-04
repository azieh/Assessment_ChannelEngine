namespace Assessment_ChannelEngine.Common.Models.ChannelEngine
{
    public class OrdersResult
    {
        public OrderResult[] Content { get; set; }
        public bool Success { get; set; }
    }

    public class OrderResult
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public LineResult[] Lines { get; set; }
    }

    public class LineResult
    {
        public string MerchantProductNo { get; set; }
        public int Quantity { get; set; }
    }
}