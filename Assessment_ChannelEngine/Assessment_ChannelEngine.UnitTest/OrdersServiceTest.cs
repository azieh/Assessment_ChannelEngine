using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment_ChannelEngine.Common.Interfaces;
using Assessment_ChannelEngine.Common.Models;
using Assessment_ChannelEngine.Common.Models.ChannelEngine;
using Assessment_ChannelEngine.Core.Interface;
using Assessment_ChannelEngine.Services;
using Assessment_ChannelEngine.UnitTest.Faker;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Assessment_ChannelEngine.UnitTest
{
    public class OrdersServiceTest
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<IGenericRestClient> _genericRestClientMock;
        private IOrdersService _sut;

        public static List<string> MerchantProductNoList => new List<string>
        {
            "001201-M", "001201-S", "001201-XL", "001201-L", "001201-XS", "001201-XXL"
        };

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "ChannelEngineApi:BaseUrl")])
                .Returns("https://unittest.com");
            _configurationMock.SetupGet(x => x[It.Is<string>(s => s == "ChannelEngineApi:ApiKey")]).Returns("1234");

            _genericRestClientMock = new Mock<IGenericRestClient>();

            _sut = new OrdersService(_configurationMock.Object, _genericRestClientMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _configurationMock = null;
            _genericRestClientMock = null;

            _sut = null;
        }

        [Test]
        public void OrdersService_throw_exception_when_Configuration_isNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OrdersService(null, _genericRestClientMock.Object));
        }

        [Test]
        public void OrdersService_throw_exception_when_GenericRestClient_isNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OrdersService(_configurationMock.Object, null));
        }

        [Test]
        public async Task OrdersService_GetTop5ProductSold_should_return_OrderResultAsync()
        {
            //Arrange
            var orderResult = new OrdersResultFaker().SeedEntities(1).First();
            _genericRestClientMock.Setup(_ => _.GetAsync<OrdersResult>(It.IsAny<string>()))
                .ReturnsAsync(() => orderResult);
            var productsResult = new ProductsResultFaker().SeedEntities(1).First();
            _genericRestClientMock.Setup(_ => _.GetAsync<ProductsResult>(It.IsAny<string>()))
                .ReturnsAsync(() => productsResult);

            //Act
            var result = await _sut.GetTop5ProductSold();

            //Assert
            Assert.IsInstanceOf<ICollection<ProductVm>>(result);
            Assert.That(result.Count, Is.LessThanOrEqualTo(5));
            Assert.That(result.Select(_ => _.Quantity).ToList(), Is.Ordered.Descending);
        }

        [Test]
        public async Task OrdersService_GetAllOrdersInStatusInProgress_should_return_OrderResultAsync()
        {
            //Arrange
            var orderResult = new OrdersResultFaker().SeedEntities(1).First();
            _genericRestClientMock.Setup(_ => _.GetAsync<OrdersResult>(It.IsAny<string>()))
                .ReturnsAsync(() => orderResult);

            //Act
            var result = await _sut.GetAllOrdersInStatusInProgress();

            //Assert
            Assert.IsInstanceOf<OrdersResult>(result);
            Assert.IsTrue(result.Content.Any());
        }

        [Test]
        public void OrdersService_GetTop5ProductSold_should_throw_exception_when_there_is_no_product_details()
        {
            //Arrange
            var orderResult = new OrdersResultFaker().SeedEntities(1).First();
            _genericRestClientMock.Setup(_ => _.GetAsync<OrdersResult>(It.IsAny<string>()))
                .ReturnsAsync(() => orderResult);

            //Act & Assert
            Assert.ThrowsAsync<ApplicationException>(async () =>
                await _sut.GetTop5ProductSold());
        }

        [Test]
        public async Task OrdersService_UpdateProductStockTo25_should_return_Ok()
        {
            //Arrange
            var productsResult = new ProductsResultFaker().SeedEntities(1).First();
            _genericRestClientMock.Setup(_ => _.GetAsync<ProductsResult>(It.IsAny<string>()))
                .ReturnsAsync(() => productsResult);

            //Act
            await _sut.UpdateProductStockTo25("001201-XL");

            //Assert
            Assert.Pass("Ok");
        }

        [Test]
        public void OrdersService_GetTop5ProductSold_should_throw_exception_when_argument_is_null()
        {
            //Arrange
            var orderResult = new OrdersResultFaker().SeedEntities(1).First();
            _genericRestClientMock.Setup(_ => _.GetAsync<OrdersResult>(It.IsAny<string>()))
                .ReturnsAsync(() => orderResult);

            //Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await _sut.UpdateProductStockTo25(null));
        }

        [Test]
        public void OrdersService_UpdateProductStockTo25_should_throw_exception_when_there_is_no_product_details()
        {
            //Arrange

            //Act & Assert
            Assert.ThrowsAsync<ApplicationException>(async () =>
                await _sut.UpdateProductStockTo25("001201-XL"));
        }
    }
}