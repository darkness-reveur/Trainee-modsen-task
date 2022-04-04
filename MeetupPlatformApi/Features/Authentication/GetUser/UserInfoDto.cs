﻿namespace MeetupPlatformApi.Features.Authentication.GetUser;

using System.ComponentModel.DataAnnotations;

public class UserInfoDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Username { get; set; }
}
