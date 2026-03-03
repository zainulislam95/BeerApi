using BeerApi.Data;
using BeerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ApplicationDbContext db, ILogger<CustomerService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                return await _db.Customers.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers");
                throw new ServiceException("Failed to retrieve customers.", ex);
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            try
            {
                return await _db.Customers.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer {Id}", id);
                throw new ServiceException("Failed to retrieve customer.", ex);
            }
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            try
            { 
                if (customer.CreatedAt == default) customer.CreatedAt = DateTime.UtcNow;
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                throw new ServiceException("Failed to create customer.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, Customer customer)
        {
            try
            {
                var existing = await _db.Customers.FindAsync(id);
                if (existing == null) return false;

                existing.FirstName = customer.FirstName;
                existing.LastName = customer.LastName;
                existing.Email = customer.Email;
                existing.Phone = customer.Phone;

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
