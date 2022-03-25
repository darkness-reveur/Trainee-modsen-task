﻿using MeetupPlatformApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DTO
{
    public class MeetupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }
    }
}