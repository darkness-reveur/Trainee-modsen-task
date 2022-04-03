namespace MeetupPlatformApi.Controllers;

using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using MeetupPlatformApi.Authentication.Manager;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;

[Route("/api/users")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;
    private readonly AuthenticationManager authenticationManager;

    public AuthenticationController(ApplicationContext context, IMapper mapper, AuthenticationManager authenticationManager)
    {
        this.context = context;
        this.mapper = mapper;
        this.authenticationManager = authenticationManager;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == id);
        var outputDto = mapper.Map<UserOutputDto>(user);
        return outputDto is not null ? Ok(outputDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registrationDto)
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

        var accessToken = authenticationManager.IssueAccessToken(user);
        var userInfoDto = mapper.Map<UserOutputDto>(user);
        var outputDto = new UserRegistrationResultDto
        {
            UserInfo = userInfoDto,
            AccessToken = accessToken
        };
        return CreatedAtAction(nameof(GetUserById), new {id = outputDto.UserInfo.Id}, outputDto);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto authenticationDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Username == authenticationDto.Username);
        if (user is null || !BCrypt.Verify(authenticationDto.Password, user.Password))
        {
            return BadRequest("Username or password is incorrect.");
        }

        var accessToken = authenticationManager.IssueAccessToken(user);
        var outputDto = new AuthenticationTokenOutputDto { AccessToken = accessToken };
        return Ok(outputDto);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        var currentUserInfo = authenticationManager.GetCurrentUserInfo(User);
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == currentUserInfo.UserId);

        var outputDto = mapper.Map<UserOutputDto>(user);
        return Ok(outputDto);
    }
}
