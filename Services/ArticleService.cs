using BeerApi.Data;
using BeerApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerApi.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(ApplicationDbContext db, ILogger<ArticleService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Item>> GetProductsAsync(string? filter, string? sortBy)
        {
            _logger.LogInformation("Fetching products from database (filter={Filter}, sortBy={SortBy})", filter, sortBy);

            try
            {
                // Load products including articles
                var query = _db.Items
                    .AsNoTracking()
                    .Include(p => p.Articles)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(filter))
                {
                    if (decimal.TryParse(filter.Replace("€/Liter", "").Replace(",", ".").Trim(), out var priceThreshold))
                    {
                        query = query.Where(p => p.Articles.Any(a => a.PricePerUnit.HasValue && a.PricePerUnit.Value >= priceThreshold));
                    }
                }

                var products = await query.ToListAsync();

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

                _logger.LogInformation("Returning {Count} items from database", products.Count);
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error querying database for products");
                throw new ServiceException("Database query failed.", ex);
            }
        }
    }
}
