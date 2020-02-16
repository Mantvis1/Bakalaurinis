using Microsoft.AspNetCore.Identity;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}