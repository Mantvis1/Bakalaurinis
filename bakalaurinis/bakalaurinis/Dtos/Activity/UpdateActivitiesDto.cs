using System.Collections.Generic;

namespace bakalaurinis.Dtos.Activity
{
    public class UpdateActivitiesDto
    {
        public ICollection<ActivityDto> Activities { get; set; }
    }
}
