namespace GarageApp.API.Models
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }

        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Vin { get; set; } = string.Empty;

        public Customer Customer { get; set; } = null!;
        public ICollection<JobCard> JobCards { get; set; } = new List<JobCard>();
    }
}