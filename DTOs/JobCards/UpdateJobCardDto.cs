namespace GarageApp.API.DTOs.JobCards
{
    public class UpdateJobCardDto
    {
        public string Complaint { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string WorkDone { get; set; } = string.Empty;
    }
}