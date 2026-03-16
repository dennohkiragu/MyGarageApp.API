using GarageApp.API.DTOs.JobCards;

namespace GarageApp.API.Interfaces
{
    public interface IJobCardService
    {
        Task<JobCardDto?> CreateAsync(CreateJobCardDto model);
        Task<List<JobCardDto>> GetAllAsync();
        Task<JobCardDto?> GetByIdAsync(Guid id);
        Task<List<JobCardDto>> GetByCustomerIdAsync(Guid customerId);
        Task<List<JobCardDto>> GetByCarIdAsync(Guid carId);
        Task<List<JobCardDto>> GetByAssignedMechanicIdAsync(string mechanicId);
        Task<JobCardDto?> AssignMechanicAsync(Guid id, AssignMechanicDto model);
        Task<JobCardDto?> UpdateAsync(Guid id, UpdateJobCardDto model);
        Task<JobCardDto?> UpdateStatusAsync(Guid id, UpdateJobCardStatusDto model);
        Task<bool> DeleteAsync(Guid id);
    }
}