﻿using System;

namespace bakalaurinis.Dtos
{
    public class GeneratorFreeSpaceDto
    {
        public GeneratorFreeSpaceDto(DateTime start, DateTime end, int duration)
        {
            Start = start;
            End = end;
            Duration = duration;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Duration { get; set; }
    }
}
