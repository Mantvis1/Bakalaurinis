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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishUntil { get; set; }
        public ActivityPriorityEnum ActivityPriority { get; set; }
    }
}
