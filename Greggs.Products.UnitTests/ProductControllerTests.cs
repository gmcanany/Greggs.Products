using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.Controllers;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Facades;
using Greggs.Products.Api.Models;

namespace Greggs.Products.UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public async Task Get_Calls_DataAccess_List_Returns_Products()
        {
            int pageStart = 0;
            int pageSize = 5;
            string[] sampleProducts = new[]
            {
                "Sausage Roll", "Vegan Sausage Roll", "Steak Bake","Yum Yum", "Pink Jammie"
            };
            var rng = new Random();
            var productList = Enumerable.Range(1, pageSize).Select(index => new Product
            {
                PriceInPounds = rng.Next(0, 10),
                Name = sampleProducts[rng.Next(sampleProducts.Length)]
            })
            .ToArray();

            var loggerMock = new Mock<ILogger<ProductController>>();
            var productServiceMock = new Mock<IDataAccess<Product>>();
            productServiceMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>())).Returns(productList).Verifiable();
            var facadeMock = new Mock<ICurrencyFacade>();
            facadeMock.Setup(x => x.CalculateProductPrices(It.IsAny<IEnumerable<Product>>(), It.IsAny<string>()))
                .Returns(productList);
            var sut = new ProductController(loggerMock.Object, productServiceMock.Object, facadeMock.Object);

            var products = sut.Get(pageStart, pageSize);

            productServiceMock.Verify(x => x.List(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            products.Should().BeOfType<Product[]>();
        }

        [Fact]
        public async Task Get_Calls_CurrencyFacade()
        {
            int pageStart = 0;
            int pageSize = 5;
            string[] sampleProducts = new[]
            {
                "Sausage Roll", "Vegan Sausage Roll", "Steak Bake","Yum Yum", "Pink Jammie"
            };
            var rng = new Random();
            var productList = Enumerable.Range(1, pageSize).Select(index => new Product
                {
                    PriceInPounds = rng.Next(0, 10),
                    Name = sampleProducts[rng.Next(sampleProducts.Length)]
                })
                .ToArray();

            var loggerMock = new Mock<ILogger<ProductController>>();
            var productServiceMock = new Mock<IDataAccess<Product>>();
            productServiceMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>())).Returns(productList).Verifiable();
            var facadeMock = new Mock<ICurrencyFacade>();
            facadeMock.Setup(x => x.CalculateProductPrices(It.IsAny<IEnumerable<Product>>(), It.IsAny<string>()))
                .Returns(productList).Verifiable();
            var sut = new ProductController(loggerMock.Object, productServiceMock.Object, facadeMock.Object);

            var products = sut.Get(pageStart, pageSize);

            facadeMock.Verify(x => x.CalculateProductPrices(It.IsAny<IEnumerable<Product>>(), It.IsAny<string>()), Times.Once());
            products.Should().BeOfType<Product[]>();
        }


    }
}
