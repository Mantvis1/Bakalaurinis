using System.Collections.Generic;

namespace bakalaurinis.Dtos.Work
{
    public class UpdateWorkDto
    {
        public ICollection<WorkDto> Activities { get; set; }
    }
}
