namespace GarageApp.API.DTOs.Lookups
{
    public class LookupMasterDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}