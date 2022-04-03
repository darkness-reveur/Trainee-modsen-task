namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using AutoMapper;
using BCrypt.Net;
using MeetupPlatformApi.Authentication.Manager;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using MeetupPlatformApi.Seedwork.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiSection(ApiSections.Authentication)]
public class RegisterNewUserFeature : FeatureBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;
    private readonly AuthenticationManager authenticationManager;

    public RegisterNewUserFeature(ApplicationContext context, IMapper mapper, AuthenticationManager authenticationManager)
    {
        this.context = context;
        this.mapper = mapper;
        this.authenticationManager = authenticationManager;
    }

    [HttpPost("/api/users")]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegistrationDto registrationDto)
    {
        var usernameAlreadyTaken = await context.Users.AnyAsync(user => user.Username == registrationDto.Username);
        if (usernameAlreadyTaken)
        {
            return BadRequest("Provided username is already taken");
        }

        var user = mapper.Map<User>(registrationDto);
        user.Password = BCrypt.HashPassword(user.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var registrationResultDto = new RegistrationResultDto
        {
            UserInfo = mapper.Map<RegistrationResultDto.UserInfoDto>(user),
            AccessToken = authenticationManager.IssueAccessToken(user)
        };
        return CreatedAtRoute("GetUser", new {id = registrationResultDto.UserInfo.Id}, registrationResultDto);
    }
}
