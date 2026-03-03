using BeerApi.Models;
using BeerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetProducts([FromQuery] string? filter, [FromQuery] string? sortBy)
        {
            _logger.LogInformation("GetProducts (DB) called (filter={Filter}, sortBy={SortBy})", filter, sortBy);

            try
            {
                var products = await _articleService.GetProductsAsync(filter, sortBy);
                return Ok(products);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Service error while getting products from DB");
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status502BadGateway, title: "Database service error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while getting products from DB");
                return Problem(detail: "An unexpected error occurred.", statusCode: StatusCodes.Status500InternalServerError, title: "Internal server error");
            }
        }
    }
}
