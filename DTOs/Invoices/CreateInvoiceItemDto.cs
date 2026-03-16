namespace GarageApp.API.DTOs.Invoices
{
    public class CreateInvoiceItemDto
    {
        public string ItemName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}