using bakalaurinis.Dtos.Work;
using System.Collections.Generic;

namespace bakalaurinis.Dtos.Schedule
{
    public class GetScheduleDto
    {
        public List<WorkDto> works = new List<WorkDto>();
        public int Busyness { get; set; }
    }
}
