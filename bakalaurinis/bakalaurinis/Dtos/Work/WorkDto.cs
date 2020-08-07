using System;
using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.Work
{
    public class WorkDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public WorkPriorityEnum WorkPriority { get; set; }
        public bool WillBeParticipant { get; set; }
        public bool IsAuthor { get; set; }
    }
}
