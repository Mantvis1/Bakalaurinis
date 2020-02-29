using bakalaurinis.Infrastructure.Utils;
using System;
using Xunit;

namespace test
{
    public class TimeServiceTests
    {
        private readonly TimeService _timeService;
        public TimeServiceTests()
        {
            _timeService = new TimeService();
        }
        [Fact]
        public void Test_GetDateTime()
        {
            var now = _timeService.GetCurrentDay();
            var time = _timeService.GetDateTime(100);

            Assert.Equal(time, new DateTime(now.Year, now.Month, now.Day).AddHours(1).AddMinutes(40));
        }
    }
}
