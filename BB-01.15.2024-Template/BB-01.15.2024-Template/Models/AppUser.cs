using Microsoft.AspNetCore.Identity;

namespace BB_01._15._2024_Template.Models
{
    public class AppUser:IdentityUser
    {
        public string Country { get; set; }
    }
}
