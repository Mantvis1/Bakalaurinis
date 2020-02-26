using bakalaurinis.Dtos.User;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IUserService
    {
        Task<AfterAutentificationDto> Authenticate(AuthenticateDto authenticateDto);
        Task<UserNameDto> GetNameById(int id);
        Task<int> Register(RegistrationDto registrationDto);
        Task<bool> Delete(int id);
        Task<GetScheduleStatus> GetStatusById(int id);
    }
}