using System;

namespace bakalaurinis.Services.Interfaces
{
    public interface ITimeService
    {
        DateTime GetCurrentDay();
        DateTime GetDateTime(int? minutes);
        DateTime AddMinutesToTime(DateTime dateTime, int minutes);
        int GetDifferentBetweenTwoDatesInMinutes(DateTime firstDate, DateTime secondDate);
        public int GetYearsToMinutes();
        public int GetMinutesInHour();
    }
}
