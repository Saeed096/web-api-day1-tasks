using Microsoft.AspNetCore.Identity;

namespace task1.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string address { get; set; }
    }
}
