namespace GarageApp.API.DTOs.Invoices
{
    public class InvoiceItemDto
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}