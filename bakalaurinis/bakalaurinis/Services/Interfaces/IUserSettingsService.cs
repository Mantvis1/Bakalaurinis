using bakalaurinis.Dtos.UserSettings;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Interfaces
{
    public interface IUserSettingsService
    {
        Task<UserSettingsDto> GetByUserId(int userId);
        Task<bool> Update(UserSettingsDto userSettingsDto);
        Task<bool> Update(UpdateUserItemsPerPageSettings userSettingsDto);
        Task<int> Create(int userId);
        Task<GetUserItemsPerPageSetting> GetUserItemsPerPageSetting(int userId);
    }
}
