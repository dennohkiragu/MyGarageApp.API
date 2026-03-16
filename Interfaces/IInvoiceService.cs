using GarageApp.API.DTOs.Invoices;

namespace GarageApp.API.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDto?> CreateAsync(CreateInvoiceDto model);
        Task<List<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto?> GetByIdAsync(Guid id);
        Task<InvoiceDto?> GetByJobCardIdAsync(Guid jobCardId);
        Task<InvoiceDto?> UpdateAsync(Guid id, UpdateInvoiceDto model);
        Task<InvoiceDto?> UpdatePaymentStatusAsync(Guid id, UpdateInvoicePaymentDto model);
        Task<bool> DeleteAsync(Guid id);
    }
}