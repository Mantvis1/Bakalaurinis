using bakalaurinis.Infrastructure.Enums;
using System;

namespace bakalaurinis.Dtos.Activity
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ActivityPriorityEnum ActivityPriority { get; set; }
        public bool WillBeParticipant { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
