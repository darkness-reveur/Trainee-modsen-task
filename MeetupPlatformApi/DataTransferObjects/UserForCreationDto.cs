﻿using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects
{
    public class UserForCreationDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
