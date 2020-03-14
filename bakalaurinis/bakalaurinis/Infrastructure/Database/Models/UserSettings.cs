using System.ComponentModel.DataAnnotations;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class UserSettings : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int StartTime { get; set; } = 8;
        [Required]
        public int EndTime { get; set; } = 22;
        [Required]
        public int ItemsPerPage { get; set; } = 10;
    }
}
