using System.Collections.Generic;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Bogus;

namespace Assessment_ChannelEngine.UnitTest.Faker
{
    public class ProductResultFaker : FakerBuilder<ProductResult>
    {
        public ProductResultFaker(string merchantProductNo)
        {
            Faker = new Faker<ProductResult>()
                    .StrictMode(false)
                    .RuleFor(u => u.Ean, f => f.Commerce.Ean8())
                    .RuleFor(u => u.MerchantProductNo, f => merchantProductNo)
                    .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                    .RuleFor(u => u.Stock, f => f.Random.Int())
                ;
        }
    }
}