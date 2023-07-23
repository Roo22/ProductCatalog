using Microsoft.AspNetCore.Identity;

namespace WebApplication5.Domain
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
