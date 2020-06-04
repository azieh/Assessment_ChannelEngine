using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Bogus;

namespace Assessment_ChannelEngine.UnitTest.Faker
{
    public class OrderResultFaker : FakerBuilder<OrderResult>
    {
        public OrderResultFaker()
        {
            Faker = new Faker<OrderResult>()
                    .StrictMode(false)
                    .RuleFor(u => u.Id, f => f.Random.Int())
                    .RuleFor(u => u.Status, f => "IN_PROGRESS")
                    .RuleFor(u => u.Lines, f => new LineResultFaker()
                        .SeedEntities(f.Random.Number(1, OrdersServiceTest.MerchantProductNoList.Count))
                        .ToArray())
                ;
        }
    }
}