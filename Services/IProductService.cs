using BeerApi.Models;

namespace BeerApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(string? filter, string? sortBy);
    }
}
