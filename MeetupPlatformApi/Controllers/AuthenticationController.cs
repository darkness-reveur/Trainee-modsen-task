using AutoMapper;
using MeetupPlatformApi.Authentification;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeetupPlatformApi.Controllers;

[Route("/api/meetups/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;
    private readonly AuthenticationManager authentificationManager;

    public AuthenticationController(ApplicationContext context, IMapper mapper, AuthenticationManager authentificationManager)
    {
        this.context = context;
        this.mapper = mapper;
        this.authentificationManager = authentificationManager;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id.Equals(id));
        var userOutput = mapper.Map<UserOutputDto>(user);

        return userOutput == null ? Ok(userOutput) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userFromBody)
    {
        var user = mapper.Map<UserEntity>(userFromBody);

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var userOutput = mapper.Map<UserOutputDto>(user);

        return CreatedAtAction(nameof(GetUserById), new { id = userOutput.Id }, userOutput);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userForAuthentificationDto)
    {
        if (!await authentificationManager.ValidateUser(userForAuthentificationDto))
        {
            return Unauthorized();
        }

        return Ok(new { Token = authentificationManager.CreateToken() });
    }

    [HttpGet("user")]
    [Authorize]
    public IActionResult GetCurrentUserInfo()
    {
        var userName = User.FindFirst(ClaimTypes.Name).Value;

        return Ok(new { Username = userName });
    }
}
