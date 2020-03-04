using AutoMapper;
using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IMapper _mapper;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public UserSettingsService(IMapper mapper, IUserSettingsRepository userSettingsRepository)
        {
            _mapper = mapper;
            _userSettingsRepository = userSettingsRepository;
        }

        public async Task<UserSettingsDto> GetByUserId(int userId)
        {
            var settings = await _userSettingsRepository.GetByUserId(userId);
            var settingsDto = _mapper.Map<UserSettingsDto>(settings);

            return settingsDto;
        }

        public async Task<bool> Update(UserSettingsDto userSettingsDto)
        {
            var settings = await _userSettingsRepository.GetByUserId(userSettingsDto.UserId);

            _mapper.Map(userSettingsDto, settings);

            await _userSettingsRepository.Update(settings);
        }
    }
}
