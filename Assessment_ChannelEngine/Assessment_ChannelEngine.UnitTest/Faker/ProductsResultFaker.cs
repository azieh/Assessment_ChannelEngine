using System.Collections.Generic;
using System.Linq;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Bogus;

namespace Assessment_ChannelEngine.UnitTest.Faker
{
    public class ProductsResultFaker : FakerBuilder<ProductsResult>
    {
        public ProductsResultFaker()
        {
            Faker = new Faker<ProductsResult>()
                    .StrictMode(false)
                    .RuleFor(u => u.Content, f => CreateCompleteProductList().ToArray())
                ;
        }

        private List<ProductResult> CreateCompleteProductList()
        {
            var result = new List<ProductResult>();
            foreach (var product in OrdersServiceTest.MerchantProductNoList)
            {
                result.Add(new ProductResultFaker(product).SeedEntities(1).First());
            }

            return result;
        }
    }
}