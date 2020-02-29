using bakalaurinis.Infrastructure.Utils.Interfaces;
using System;

namespace bakalaurinis.Infrastructure.Utils
{
    public class TimeService : ITimeService
    {
        public DateTime GetCurrentDay()
        {
            return DateTime.Today;
        }

        public DateTime GetDateTime(int? minutes)
        {
            var date = new DateTime(GetCurrentDay().Year, GetCurrentDay().Month, GetCurrentDay().Day).AddMinutes(minutes.Value);

            return date;
        }
    }
}
