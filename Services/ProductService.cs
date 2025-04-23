using System.Text.Json; 
using BeerApi.Models;

namespace BeerApi.Services
{
    public class ProductService
    {
        private const string ProductDataUrl = "https://flapotest.blob.core.windows.net/test/ProductData.json";

        public async Task<List<Product>> GetProductsAsync(string? filter, string? sortBy)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(ProductDataUrl);

            var products = JsonSerializer.Deserialize<List<Product>>(response);

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

            return products;
        }
    }
}