﻿namespace MeetupPlatformApi.Authentication.Helpers;

public class TokenPair
{
    public string AccessToken { get; init; }

    public string RefreshToken { get; init; }
}
