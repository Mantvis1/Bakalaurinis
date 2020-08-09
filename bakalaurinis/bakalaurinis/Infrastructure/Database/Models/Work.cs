using bakalaurinis.Infrastructure.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bakalaurinis.Infrastructure.Database.Models
{
    public class Work : BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public int DurationInMinutes { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public WorkPriorityEnum WorkPriority { get; set; }
        [DefaultValue(false)]
        public bool WillBeParticipant { get; set; }
        [DefaultValue(true)]
        public bool IsAuthor { get; set; }
        public ICollection<Invitation> Invitations { get; set; }

        public Work DeepCopy()
        {
            return new Work
            {
                UserId = UserId,
                Title = Title,
                WorkPriority = WorkPriority,
                Description = Description,
                DurationInMinutes = DurationInMinutes
            };
        }
    }
}
