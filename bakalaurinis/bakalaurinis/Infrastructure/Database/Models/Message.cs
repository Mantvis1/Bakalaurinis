using System;
using System.ComponentModel.DataAnnotations;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Message : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
