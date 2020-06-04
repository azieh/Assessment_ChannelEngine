using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Bogus;

namespace Assessment_ChannelEngine.UnitTest.Faker
{
    public class OrdersResultFaker : FakerBuilder<OrdersResult>
    {
        public OrdersResultFaker()
        {
            Faker = new Faker<OrdersResult>()
                    .StrictMode(false)
                    .RuleFor(u => u.Content, f => new OrderResultFaker().SeedEntities(10).ToArray())
                    .RuleFor(u => u.Success, f => true)
                ;
        }
    }
}