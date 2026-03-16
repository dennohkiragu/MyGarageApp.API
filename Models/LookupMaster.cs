namespace GarageApp.API.Models
{
    public class LookupMaster
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<LookupItem> Items { get; set; } = new List<LookupItem>();
    }
}