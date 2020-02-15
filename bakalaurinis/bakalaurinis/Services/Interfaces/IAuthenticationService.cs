using bakalaurinis.Infrastructure.Database.Models;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Authenticate(string email, string password);
    }
}
