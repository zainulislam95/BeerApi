using BeerApi.Dtos;

namespace BeerApi.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CustomerCreateDto customer);
        Task<bool> UpdateAsync(int id, CustomerUpdateDto customer);
        Task<bool> DeleteAsync(int id);
    }
}
