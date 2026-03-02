using System.Text.Json;
using System.Linq;
using BeerApi.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace BeerApi.Services
{
    public class ProductService : IProductService
    {
        private const string ProductDataUrl = "https://flapotest.blob.core.windows.net/test/ProductData.json";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<Product>> GetProductsAsync(string? filter, string? sortBy)
        {
            _logger.LogInformation("Fetching products (filter={Filter}, sortBy={SortBy})", filter, sortBy);

            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetStringAsync(ProductDataUrl);

                var products = JsonSerializer.Deserialize<List<Product>>(response) ?? new List<Product>();

                if (!string.IsNullOrEmpty(filter))
                {
                    if (decimal.TryParse(filter.Replace("€/Liter", "").Replace(",", ".").Trim(), out var priceThreshold))
                    {
                        products = products.Where(p => p.Articles.Any(a => a.PricePerUnit >= priceThreshold)).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.ToLower())
                    {
                        case "priceasc":
                            products = products.OrderBy(p => p.Articles.Any() ? p.Articles.Min(a => a.Price) : decimal.MaxValue).ToList();
                            break;
                        case "pricedesc":
                            products = products.OrderByDescending(p => p.Articles.Any() ? p.Articles.Min(a => a.Price) : decimal.MinValue).ToList();
                            break;
                    }
                }

                _logger.LogInformation("Returning {Count} products", products.Count);
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products from {Url}", ProductDataUrl);
                throw new ServiceException("Failed to fetch products.", ex);
            }
        }
    }
}
