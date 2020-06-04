namespace Assessment_ChannelEngine.Common.Models.ChannelEngine
{
    public class OrdersResult
    {
        public Order[] Content { get; set; }
        public bool Success { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Line[] Lines { get; set; }
    }

    public class Line
    {
        public string MerchantProductNo { get; set; }
        public int Quantity { get; set; }
    }
}