namespace GarageApp.API.Models
{
    public class JobCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }

        public string? AssignedMechanicId { get; set; }

        public string Complaint { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string WorkDone { get; set; } = string.Empty;

        public JobCardStatus Status { get; set; } = JobCardStatus.Pending;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public Customer Customer { get; set; } = null!;
        public Car Car { get; set; } = null!;
        public Invoice? Invoice { get; set; }
    }
}