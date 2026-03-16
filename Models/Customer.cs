namespace GarageApp.API.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        
        public ICollection<Car> Cars { get; set; } = new List<Car>();
        public ICollection<JobCard> JobCards { get; set; } = new List<JobCard>();
    }
}