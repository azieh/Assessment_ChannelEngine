using Bogus;
using System.Collections.Generic;

namespace Assessment_ChannelEngine.UnitTest.Faker
{
    public abstract class FakerBuilder<TModel>
        where TModel : class
    {
        protected Faker<TModel> Faker;

        public List<TModel> SeedEntities(int size)
        {
            return Faker.Generate(size);
        }
    }
}