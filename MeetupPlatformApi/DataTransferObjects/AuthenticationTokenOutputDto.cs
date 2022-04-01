﻿using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects
{
    public class AuthenticationTokenOutputDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
