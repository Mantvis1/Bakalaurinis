using bakalaurinis.Dtos.User;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IUserService
    {
        Task<AfterAutentificationDto> Authenticate(string username, string password);
    }
}