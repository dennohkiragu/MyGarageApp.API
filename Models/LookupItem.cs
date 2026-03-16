namespace GarageApp.API.Models
{
    public class LookupItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LookupMasterId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }

        public LookupMaster LookupMaster { get; set; } = null!;
    }
}