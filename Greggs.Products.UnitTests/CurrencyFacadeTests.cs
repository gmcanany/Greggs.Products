using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Greggs.Products.Api.Facades;
using Greggs.Products.Api.Models;

namespace Greggs.Products.UnitTests
{
    public class CurrencyFacadeTests
    {

        [Fact]
        public async Task CalculateProductPrices_Returns_Product_Prices_In_Currency()
        {
            string currencyCode = "EUR";
            decimal exchangeRate = 1.11m;

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

            var sut = new CurrencyFacade();

            var products = sut.CalculateProductPrices(productList, currencyCode);

            products.Should().BeOfType<Product[]>();
            products.FirstOrDefault().Currency.Should().Be(currencyCode);
            products.FirstOrDefault().Price.Should().Be(products.FirstOrDefault().PriceInPounds * exchangeRate);

        }


    }
}
