using bakalaurinis.Infrastructure.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public string Token { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Invitation> Invitations { get; set; }
    }
}