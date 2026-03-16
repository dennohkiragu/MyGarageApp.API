namespace GarageApp.API.DTOs.Invoices
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public Guid JobCardId { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CarDisplayName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DateCreated { get; set; }

        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}