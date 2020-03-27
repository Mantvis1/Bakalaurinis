using bakalaurinis.Infrastructure.Enums;
using System;

namespace bakalaurinis.Dtos.Activity
{
    public class NewActivityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int DurationInMinutes { get; set; }
        public ActivityPriorityEnum ActivityPriority { get; set; }
        public bool WillBeParticipant { get; set; }
    }
}
