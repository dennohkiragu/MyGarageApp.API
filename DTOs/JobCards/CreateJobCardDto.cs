namespace GarageApp.API.DTOs.JobCards
{
    public class CreateJobCardDto
    {
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public string Complaint { get; set; } = string.Empty;
    }
}