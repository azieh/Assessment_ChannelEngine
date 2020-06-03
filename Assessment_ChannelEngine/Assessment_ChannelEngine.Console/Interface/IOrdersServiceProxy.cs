namespace Assessment_ChannelEngine.Console.Interface
{
    public interface IOrdersServiceProxy
    {
        public void SetQuantityTo25();
        public void GetTop5SoldProductByQuantity();
        public void FetchAllIN_PROGRESS();
    }
}