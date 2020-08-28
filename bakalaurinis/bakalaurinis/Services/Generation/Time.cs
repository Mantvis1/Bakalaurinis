using System;

namespace bakalaurinis.Services.Generation
{
    public class Time
    {
        private int Start { get; set; }
        private int End { get; set; }

        public Time(int start, int end)
        {
            Start = start;
            End = end;
        }

        public void Update(int? start = null, int? end = null)
        {
            if(start != null)
            {
                UpdateStart(start.Value);
            }

            if (end != null)
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
    }
}
