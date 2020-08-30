using bakalaurinis.Helpers;
using bakalaurinis.Infrastructure.Enums;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;

namespace bakalaurinis.Services.Generation
{
    public class Time
    {

        private int Start { get; set; }
        private int End { get; set; }
        private int CurrentDay { get; set; }

        public Time(int start, int end)
        {
            Start = start;
            End = end;
            CurrentDay = 0;
        }

        public void Update(int? start = null, int? end = null)
        {
            if (!CompareValues.IsNull(start))
            {
                UpdateStart(start.Value);
            }

            if (!CompareValues.IsNull(end))
            {
                UpdateStart(end.Value);
            }
        }

        public void UpdateStart(int start)
        {
            Start = start;
        }

        public void UpdateEnd(int end)
        {
            End = end;
        }

        public int GetStart()
        {
            return Start;
        }

        public int GetEnd()
        {
            return End;
        }

        public void AddOneDayToCurrent()
        {
            CurrentDay++;
        }

        public void MoveToNextDay(int startTimeSetting, int endTimeSetting)
        {
            Start = startTimeSetting * (int)TimeEnum.MinutesInHour + CurrentDay * (int)TimeEnum.HoursInDay * (int)TimeEnum.MinutesInHour;
            End = endTimeSetting * (int)TimeEnum.MinutesInHour + CurrentDay * (int)TimeEnum.HoursInDay * (int)TimeEnum.MinutesInHour;
        }

        public void UpdateCurrentDay(int currentDay)
        {
            CurrentDay = currentDay;
        }

    }
}
