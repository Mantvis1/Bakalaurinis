namespace bakalaurinis.Infrastructure.Database.Models
{
    public class UserSettings : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int StartTime { get; set; } = 8;
        public int EndTime { get; set; } = 22;
    }
}
