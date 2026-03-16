using GarageApp.API.DTOs.Customers;

namespace GarageApp.API.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateAsync(CreateCustomerDto model);
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(Guid id);
        Task<CustomerDto?> UpdateAsync(Guid id, UpdateCustomerDto model);
        Task<bool> DeleteAsync(Guid id);
    }
}