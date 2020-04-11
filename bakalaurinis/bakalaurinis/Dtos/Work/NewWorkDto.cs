using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.Work
{
    public class NewWorkDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int DurationInMinutes { get; set; }
        public ActivityPriorityEnum ActivityPriority { get; set; }
        public bool WillBeParticipant { get; set; }
    }
}
