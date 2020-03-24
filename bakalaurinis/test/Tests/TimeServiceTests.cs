using bakalaurinis.Services;
using System;
using Xunit;

namespace test.Tests
{
    public class TimeServiceTests
    {
        private readonly TimeService _timeService;
        public TimeServiceTests()
        {
            var setUp = new SetUp();
            setUp.Initialize();

            _timeService = new TimeService();
        }
        [Fact]
        public void GetDateTime_TimePlus100Minutes()
        {
            var now = _timeService.GetCurrentDay();
            var time = _timeService.GetDateTime(100);

            Assert.Equal(time, new DateTime(now.Year, now.Month, now.Day).AddHours(1).AddMinutes(40));
        }

        [Fact]
        public void GetCurrentDay_DayIsTheSame()
        {
            var now = _timeService.GetCurrentDay();
            var currentDay = DateTime.Today;

            Assert.Equal(currentDay, now);
        }

        [Theory]
        [InlineData(65)]
        public void DifferentBetweenDates_65Minutes(int result)
        {
            var startDate = new DateTime(2020, 10, 10, 11, 20, 0);
            var endDate = new DateTime(2020, 10, 10, 12, 25, 0);
            var realTimeDifferent = _timeService.GetDiferrentBetweenTwoDatesInMinutes(startDate, endDate);

            Assert.Equal(realTimeDifferent, result);
        }

        [Fact]
        public void AddMinutesToTime_CurrenTimePlus65Minutes()
        {
            var currenTime = new DateTime(2020, 10, 10, 11, 20, 0);
            var realTimeDifferent = _timeService.AddMinutesToTime(currenTime, 65);

            Assert.Equal(realTimeDifferent, currenTime.AddMinutes(65));
        }

    }
}
