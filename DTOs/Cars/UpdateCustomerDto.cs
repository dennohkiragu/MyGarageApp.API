namespace GarageApp.API.DTOs.Cars
{
    public class UpdateCarDto
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Vin { get; set; } = string.Empty;
    }
}