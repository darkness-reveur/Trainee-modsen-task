﻿using System.ComponentModel.DataAnnotations;

namespace MeetupPlatformApi.DataTransferObjects;

public class UserOutputDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }
}
