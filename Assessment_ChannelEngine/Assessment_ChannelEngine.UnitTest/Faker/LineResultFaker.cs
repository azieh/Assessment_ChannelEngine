using System.Collections.Generic;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Bogus;

namespace Assessment_ChannelEngine.UnitTest.Faker
{
    public class LineResultFaker : FakerBuilder<LineResult>
    {
        public LineResultFaker()
        {
            Faker = new Faker<LineResult>()
                    .StrictMode(false)
                    .RuleFor(u => u.Quantity, f => f.Random.Number(1, 20))
                    .RuleFor(u => u.MerchantProductNo, f => f.PickRandom(OrdersServiceTest.MerchantProductNoList))
                ;
        }
    }
}