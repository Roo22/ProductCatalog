using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApplication5.Models
{
    public class User:IdentityUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Product> Product { get; set; }


    }
}
