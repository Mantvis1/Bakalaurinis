using bakalaurinis.Infrastructure.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Activity : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FinishUntil { get; set; }
        public ActivityPriorityEnum ActivityPriority { get; set; }
    }
}
