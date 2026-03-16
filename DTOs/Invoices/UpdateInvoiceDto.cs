namespace GarageApp.API.DTOs.Invoices
{
    public class UpdateInvoiceDto
    {
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }
}