using bakalaurinis.Dtos.UserSettings;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Xunit;

namespace test.Tests
{
    public class UserSettingsServiceTests
    {
        private readonly UserSettingsService _userSettingsService;

        public UserSettingsServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;

            var userSettingsRepository = new UserSettingsRepository(context);

            _userSettingsService = new UserSettingsService(mapper, userSettingsRepository);
        }

        [Theory]
        [InlineData(1)]
        public async void GetSettingsByUserId_SettingsDto(int userId)
        {
            var settingsDto = await _userSettingsService.GetByUserId(userId);

            Assert.NotNull(settingsDto);
        }

        [Theory]
        [InlineData(1)]
        public async void GetItemsPerPageSettingByUserId_ItemsPerPageDto(int userId)
        {
            var itemsPerPageDto = await _userSettingsService.GetUserItemsPerPageSetting(userId);

            Assert.NotNull(itemsPerPageDto);
        }

        [Theory]
        [InlineData(1, 9, 11)]
        public async void UpdateTimeSettings_SettingsWereUpdated(int userId, int startTime, int endTime)
        {
            var updateSettingsDto = new UserSettingsDto()
            {
                UserId = 1,
                StartTime = startTime,
                EndTime = endTime
            };

            await _userSettingsService.Update(updateSettingsDto);
            var userSettingsDto = await _userSettingsService.GetByUserId(userId);

            Assert.Equal(userSettingsDto.EndTime, endTime);
            Assert.Equal(userSettingsDto.StartTime, startTime);
        }

        [Theory]
        [InlineData(1, 10)]
        public async void UpdateItemsPerPAge_SettingsWereUpdated(int userId, int itemsPerPage)
        {
            var updateItemsPerPageDto = new UpdateUserItemsPerPageSettings()
            {
                UserId = 1,
                ItemsPerPage = itemsPerPage
            };

            await _userSettingsService.Update(updateItemsPerPageDto);
            var itemsPerPageDto = await _userSettingsService.GetUserItemsPerPageSetting(userId);

            Assert.Equal(itemsPerPageDto.ItemsPerPage, itemsPerPage);
        }
    }
}
