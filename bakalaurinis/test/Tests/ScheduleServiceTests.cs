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



            var worksRepository = new WorksRepository(context);
            _scheduleService = new ScheduleService(worksRepository, mapper);
        }

        [Theory]
        [InlineData(3)]
        public async void GetAllByUserId_CountsAreEquals(int id)
        {
            var scheduleLength = (await _scheduleService.GetAllByUserIdFilterByDate(id, DateTime.MinValue)).Count;

            Assert.True(scheduleLength == 1);
        }
    }
}
