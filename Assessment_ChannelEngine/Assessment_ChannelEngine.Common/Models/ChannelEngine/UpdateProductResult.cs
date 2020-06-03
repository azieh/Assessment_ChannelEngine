namespace Assessment_ChannelEngine.Common.Models.ChannelEngine
{

    public class UpdateProductResult
    {
        public UpdateResult Content { get; set; }
        public bool Success { get; set; }
    }

    public class UpdateResult
    {
        public int RejectedCount { get; set; }
        public int AcceptedCount { get; set; }
        
    }
}