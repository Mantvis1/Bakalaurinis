using bakalaurinis.Infrastructure.Enums;
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
            var date = new DateTime(GetCurrentDay().Year, GetCurrentDay().Month, GetCurrentDay().Day);

            if (minutes != null)
            {
                date = date.AddMinutes(minutes.Value);
            }

            return date;
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
