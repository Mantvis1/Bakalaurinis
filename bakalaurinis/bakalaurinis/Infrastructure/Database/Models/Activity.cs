using bakalaurinis.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required]
        public int DurationInMinutes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ActivityPriorityEnum ActivityPriority { get; set; }
        [DefaultValue(false)]
        public bool IsFinished { get; set; }
     //   public ICollection<Invitations> Invitations { get; set; }
    }
}
