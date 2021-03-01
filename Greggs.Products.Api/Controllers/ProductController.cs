using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Extensions;
using Greggs.Products.Api.Facades;
using Greggs.Products.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IDataAccess<Product> _productService;
        private readonly ICurrencyFacade _currencyFacade;
        
        public ProductController(ILogger<ProductController> logger, IDataAccess<Product> productService, ICurrencyFacade currencyFacade)
        {
            _logger = logger;
            _productService = productService;
            _currencyFacade = currencyFacade;
        }

        [HttpGet]
        public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5, string currencyCode = "GBP")
        {

            _logger.LogInformation($"Get products called with pageStart {pageStart}, pageSize {pageSize}.");

            try
            {
                var products = _productService.List(pageStart, pageSize);

                products = _currencyFacade.CalculateProductPrices(products, currencyCode);

                return products;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while fetching product list with pageStart {pageStart}, pageSize {pageSize}, currency {currencyCode}");
                return new List<Product>();
            }
        }


    }
}
