using bakalaurinis.Infrastructure.Enums;

namespace bakalaurinis.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ScheduleStatusEnum ScheduleStatus { get; set; }

    }
}
