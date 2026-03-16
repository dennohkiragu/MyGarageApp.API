namespace GarageApp.API.DTOs.Lookups
{
    public class UpdateLookupItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
    }
}