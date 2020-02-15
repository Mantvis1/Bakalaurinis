using Microsoft.AspNetCore.Identity;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class User : IdentityUser
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}