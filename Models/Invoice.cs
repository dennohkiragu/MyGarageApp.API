namespace GarageApp.API.Models
{
    public class Invoice
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid JobCardId { get; set; }

        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public JobCard JobCard { get; set; } = null!;
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}