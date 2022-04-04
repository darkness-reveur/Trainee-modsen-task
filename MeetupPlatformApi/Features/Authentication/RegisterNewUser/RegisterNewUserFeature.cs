namespace MeetupPlatformApi.Features.Authentication.RegisterNewUser;

using AutoMapper;
using BCrypt.Net;
using MeetupPlatformApi.Authentication.Helpers;
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
    private readonly TokenHelper tokenHelper;

    public RegisterNewUserFeature(ApplicationContext context, IMapper mapper, TokenHelper tokenHelper)
    {
        this.context = context;
        this.mapper = mapper;
        this.tokenHelper = tokenHelper;
    }

    [HttpPost("/api/users")]
    [ProducesResponseType(typeof(RegistrationResultDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            AccessToken = tokenHelper.IssueAccessToken(user)
        };
        return Created(registrationResultDto);
    }
}
