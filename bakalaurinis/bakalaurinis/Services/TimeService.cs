using bakalaurinis.Services.Interfaces;
using System;

namespace bakalaurinis.Services
{
    
    public class TimeService : ITimeService
    {
        private readonly int _yearsToMinutes;
        private readonly int _minutesInHour;
        private readonly int DayToMinutes = 1440;
        public TimeService()
        {
            _minutesInHour = 60;
            _yearsToMinutes = DayToMinutes * 365;
        }

        public int GetYearsToMinutes()
        {
            return _yearsToMinutes;
        }

        public int GetMinutesInHour()
        {
            return _minutesInHour;
        }

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
            return dateTime.Hour * _minutesInHour + dateTime.Minute;
        }
    }
}
