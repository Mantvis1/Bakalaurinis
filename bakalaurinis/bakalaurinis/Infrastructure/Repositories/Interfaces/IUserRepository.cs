using bakalaurinis.Dtos.User;
using bakalaurinis.Infrastructure.Database.Models;
using System.Threading.Tasks;

namespace bakalaurinis.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByNameAndPassword(AuthenticateDto authenticateDto);
    }
}
