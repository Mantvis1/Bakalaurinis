using System;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class UserSettings : BaseEntity
    {
        public DayOfWeek Day { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
