﻿namespace MeetupPlatformApi.Dto
{
    public class MeetupOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }
    }
}
