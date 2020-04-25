using System;
using bakalaurinis.Infrastructure.Repositories;
using bakalaurinis.Services;
using Xunit;

namespace test.Tests
{
    public class ScheduleServiceTests
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            var context = setUp.DatabaseContext;
            var mapper = setUp.Mapper;

            var settingsRepository = new UserSettingsRepository(context);
            var worksRepository = new WorksRepository(context);
            _scheduleService = new ScheduleService(worksRepository, mapper, settingsRepository);
        }

        [Theory]
        [InlineData(3)]
        public async void GetAllByUserId_CountsAreEquals(int id)
        {
            var scheduleLength = (await _scheduleService.GetAllByUserIdFilterByDate(id, DateTime.MinValue)).works.Count;

            Assert.True(scheduleLength == 1);
        }

        [Theory]
        [InlineData(3, 50)]
        public async void GetDayBusyness_CountsAreEquals(int id, int expectedValue)
        {
            var busynessInPersentage = await _scheduleService.GetBusyness(id, DateTime.MinValue);

            Assert.True(expectedValue == busynessInPersentage);
        }
    }
}
