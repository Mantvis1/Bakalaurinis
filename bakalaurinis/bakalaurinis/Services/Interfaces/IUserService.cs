using bakalaurinis.Dtos.User;
using bakalaurinis.Infrastructure.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IUserService
    {
        Task<AfterAutenticationDto> Authenticate(AuthenticateDto authenticateDto);
        Task<UserNameDto> GetNameById(int id);
        Task<int> Register(RegistrationDto registrationDto);
        Task<bool> Delete(int id);
        Task<GetScheduleStatus> GetStatusById(int id);
        Task<ICollection<User>> GetAll();
    }
}