namespace GarageApp.API.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        public Guid JobCardId { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }
}