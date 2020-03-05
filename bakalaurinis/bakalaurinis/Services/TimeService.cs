using bakalaurinis.Services.Interfaces;
using System;

namespace bakalaurinis.Services
{
    public class TimeService : ITimeService
    {
        public DateTime AddMinutesToTime(DateTime dateTime, int minutes)
        {
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes(minutes);

            return date;
        }

        public DateTime GetCurrentDay()
        {
            return DateTime.Today;
        }

        public DateTime GetDateTime(int? minutes)
        {
            var date = new DateTime(GetCurrentDay().Year, GetCurrentDay().Month, GetCurrentDay().Day).AddMinutes(minutes.Value);

            return date;
        }

        public int GetDiferrentBetweenTwoDatesInMinutes(DateTime firstDate, DateTime secondDate)
        {
            var timeSpan = secondDate - firstDate;

            return (int)timeSpan.TotalMinutes;
        }
    }
}
