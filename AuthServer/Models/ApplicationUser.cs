using Microsoft.AspNetCore.Identity;

namespace AuthServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Team { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
    }
}
