using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Extensions
{
    public static class ProductExtension
    {
        public static void CalculatePrice(this Product product, string currencyCode, decimal exchangeRate)
        {
            product.Currency = currencyCode;
            product.Price = product.PriceInPounds * exchangeRate;
        }

        public static void CalculatePrices(this IEnumerable<Product> products, string currencyCode, decimal exchangeRate)
        {
            foreach (var product in products)
            {
                product.CalculatePrice(currencyCode, exchangeRate);
            }
        }
    }
}
