namespace GarageApp.API.DTOs.Lookups
{
    public class CreateLookupItemDto
    {
        public Guid LookupMasterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }
    }
}