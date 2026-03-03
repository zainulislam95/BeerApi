using BeerApi.Models;

namespace BeerApi.Services
{
    public interface IArticleService
    {
        Task<List<Item>> GetProductsAsync(string? filter, string? sortBy);
    }
}
