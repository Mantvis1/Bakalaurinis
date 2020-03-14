using AutoMapper;
using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Infrastructure.Database.Models;
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

        public async Task<int> Create(int userId)
        {
            var userSettings = new UserSettings
            {
                UserId = userId
            };

            return await _userSettingsRepository.Create(userSettings);
        }

        public async Task<UserSettingsDto> GetByUserId(int userId)
        {
            var settings = await _userSettingsRepository.GetByUserId(userId);
            var settingsDto = _mapper.Map<UserSettingsDto>(settings);

            return settingsDto;
        }

        public async Task<GetUserItemsPerPageSetting> GetUserItemsPerPageSetting(int userId)
        {
            var settings = await _userSettingsRepository.GetByUserId(userId);
            var settingsDto = _mapper.Map<GetUserItemsPerPageSetting>(settings);

            return settingsDto;
        }

        public async Task<bool> Update(UserSettingsDto userSettingsDto)
        {
            var settings = await _userSettingsRepository.GetByUserId(userSettingsDto.UserId);

            _mapper.Map(userSettingsDto, settings);

           return await _userSettingsRepository.Update(settings);
        }

        public async Task<bool> Update(UpdateUserItemsPerPageSettings userSettingsDto)
        {
            var settings = await _userSettingsRepository.GetByUserId(userSettingsDto.UserId);

            _mapper.Map(userSettingsDto, settings);

            return await _userSettingsRepository.Update(settings);
        }
    }
}
