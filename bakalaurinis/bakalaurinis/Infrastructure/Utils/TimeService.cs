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
    }
}
