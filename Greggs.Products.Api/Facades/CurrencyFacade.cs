using System.Collections.Generic;
using Greggs.Products.Api.Extensions;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Facades
{
    public class CurrencyFacade : ICurrencyFacade
    {
        private const decimal exchangeRateEuro = 1.11m;

        public IEnumerable<Product> CalculateProductPrices(IEnumerable<Product> products, string currencyCode)
        {
            decimal exchangeRate = 1m;

            if (currencyCode == "EUR")
            {
                exchangeRate = exchangeRateEuro;
            }

            products?.CalculatePrices(currencyCode, exchangeRate);
            return products;
        }
    }
}
