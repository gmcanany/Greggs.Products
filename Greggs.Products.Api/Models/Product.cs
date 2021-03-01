using System;

namespace Greggs.Products.Api.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal PriceInPounds { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }

}
