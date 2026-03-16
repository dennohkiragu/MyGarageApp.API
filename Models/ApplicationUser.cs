using Microsoft.AspNetCore.Identity;

namespace GarageApp.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}