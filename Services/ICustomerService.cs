using BeerApi.Dtos;

namespace BeerApi.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CustomerDto customer);
        Task<bool> UpdateAsync(int id, CustomerDto customer);
        Task<bool> DeleteAsync(int id);
    }
}
