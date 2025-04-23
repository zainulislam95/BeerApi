using BeerApi.Models;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc; 

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? filter, [FromQuery] string? sortBy)
        {
            var products = await _productService.GetProductsAsync(filter, sortBy);
            return Ok(products);
        }
    }
}