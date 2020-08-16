using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Services.Interfaces;
using System;

namespace bakalaurinis.Services
{
    public class TimeService : ITimeService
    {
        public DateTime AddMinutesToTime(DateTime dateTime, int minutes)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0).AddMinutes(minutes);
        }

        public DateTime GetCurrentDay()
        {
            return DateTime.Today;
        }

        public DateTime GetDateTime(int minutes)
        {
            return new DateTime(GetCurrentDay().Year, GetCurrentDay().Month, GetCurrentDay().Day).AddMinutes(minutes);
        }

        public int GetDifferentBetweenTwoDatesInMinutes(DateTime firstDate, DateTime secondDate)
        {
            return (int)(secondDate - firstDate).TotalMinutes;
        }

        public int GetTimeInMinutes(DateTime dateTime)
        {
            return dateTime.Hour * (int)TimeEnum.MinutesInHour + dateTime.Minute;
        }
    }
}
