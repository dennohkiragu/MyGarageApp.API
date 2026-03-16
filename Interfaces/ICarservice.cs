using GarageApp.API.DTOs.Cars;

namespace GarageApp.API.Interfaces
{
    public interface ICarService
    {
        Task<CarDto?> CreateAsync(CreateCarDto model);
        Task<List<CarDto>> GetAllAsync();
        Task<CarDto?> GetByIdAsync(Guid id);
        Task<List<CarDto>> GetByCustomerIdAsync(Guid customerId);
        Task<CarDto?> UpdateAsync(Guid id, UpdateCarDto model);
        Task<bool> DeleteAsync(Guid id);
    }
}