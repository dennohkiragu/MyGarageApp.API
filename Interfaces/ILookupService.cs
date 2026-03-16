using GarageApp.API.DTOs.Lookups;

namespace GarageApp.API.Interfaces
{
    public interface ILookupService
    {
        Task<LookupMasterDto> CreateMasterAsync(CreateLookupMasterDto model);
        Task<List<LookupMasterDto>> GetMastersAsync();
        Task<LookupMasterDto?> GetMasterByIdAsync(Guid id);
        Task<LookupMasterDto?> UpdateMasterAsync(Guid id, UpdateLookupMasterDto model);
        Task<bool> DeleteMasterAsync(Guid id);

        Task<LookupItemDto?> CreateItemAsync(CreateLookupItemDto model);
        Task<List<LookupItemDto>> GetItemsAsync();
        Task<LookupItemDto?> GetItemByIdAsync(Guid id);
        Task<LookupItemDto?> UpdateItemAsync(Guid id, UpdateLookupItemDto model);
        Task<bool> DeleteItemAsync(Guid id);

        Task<List<LookupItemDto>> GetItemsByMasterCodeAsync(string masterCode, string? search);
    }
}