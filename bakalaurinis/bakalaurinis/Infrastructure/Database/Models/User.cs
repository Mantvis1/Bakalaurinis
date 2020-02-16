using bakalaurinis.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public ScheduleStatusEnum ScheduleStatus { get; set; }
        public string Token { get; set; }
    }
}