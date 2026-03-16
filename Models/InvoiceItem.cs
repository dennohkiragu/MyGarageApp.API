namespace GarageApp.API.Models
{
    public class InvoiceItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid InvoiceId { get; set; }

        public string ItemName { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public Invoice Invoice { get; set; } = null!;
    }
}