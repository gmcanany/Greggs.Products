using System.Collections.Generic;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Facades
{
    public interface ICurrencyFacade
    {
        IEnumerable<Product> CalculateProductPrices(IEnumerable<Product> products, string currencyCode);
    }
}