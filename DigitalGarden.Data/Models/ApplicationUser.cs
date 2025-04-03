using Microsoft.AspNetCore.Identity;

namespace DigitalGarden.Models
{
    public class ApplicationUser : IdentityUser
    {
         public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public DateTime DateRegistered { get; set; } = DateTime.Now;
        public bool IsGardener { get; set; } = true;
        public string? GardeningExperience { get; set; }
        public bool IsAdmin {get; set;} = false;
    }
}
