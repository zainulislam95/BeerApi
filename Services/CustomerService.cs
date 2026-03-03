using AutoMapper;
using BeerApi.Data;
using BeerApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeerApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext db, ILogger<CustomerService> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<BeerApi.Dtos.CustomerDto>> GetAllAsync()
        {
            try
            {
                var entities = await _db.Customers.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
                return _mapper.Map<List<BeerApi.Dtos.CustomerDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers");
                throw new ServiceException("Failed to retrieve customers.", ex);
            }
        }



        public async Task<BeerApi.Dtos.CustomerDto?> GetByIdAsync(int id)
        {
            try
            {
                var ent = await _db.Customers.FindAsync(id);
                return ent == null ? null : _mapper.Map<BeerApi.Dtos.CustomerDto>(ent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer {Id}", id);
                throw new ServiceException("Failed to retrieve customer.", ex);
            }
        }

        public async Task<BeerApi.Dtos.CustomerDto> CreateAsync(BeerApi.Dtos.CustomerDto customer)
        {
            try
            {
                var entity = _mapper.Map<CustomerEntity>(customer);
                if (entity.CreatedAt == default) entity.CreatedAt = DateTime.UtcNow;
                _db.Customers.Add(entity);
                await _db.SaveChangesAsync();
                return _mapper.Map<BeerApi.Dtos.CustomerDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                throw new ServiceException("Failed to create customer.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, BeerApi.Dtos.CustomerDto customer)
        {
            try
            {
                var existing = await _db.Customers.FindAsync(id);
                if (existing == null) return false;

                // map updated fields onto existing entity
                _mapper.Map(customer, existing);

                _db.Customers.Update(existing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer {Id}", id);
                throw new ServiceException("Failed to update customer.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existing = await _db.Customers.FindAsync(id);
                if (existing == null) return false;

                _db.Customers.Remove(existing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer {Id}", id);
                throw new ServiceException("Failed to delete customer.", ex);
            }
        }
    }
}
