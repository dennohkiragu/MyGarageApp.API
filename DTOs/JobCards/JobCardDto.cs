using GarageApp.API.Models;

namespace GarageApp.API.DTOs.JobCards
{
    public class JobCardDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }

        public string? AssignedMechanicId { get; set; }
        public string AssignedMechanicName { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;
        public string CarDisplayName { get; set; } = string.Empty;

        public string Complaint { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string WorkDone { get; set; } = string.Empty;

        public JobCardStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}