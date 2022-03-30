namespace MeetupPlatformApi.Controllers;

using AutoMapper;
using MeetupPlatformApi.Authentication;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BCrypt.Net;

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
        return outputDto is null ? Ok(outputDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userFromBody)
    {
        var user = mapper.Map<UserEntity>(userFromBody);
        user.Password = BCrypt.HashPassword(user.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        var token = authenticationManager.CreateToken(user);
        var outputDto = mapper.Map<UserOutputDto>(user);
        var registrationResultDto = new UserRegistrationResultDto { UserInfo = outputDto, AccessToken = token };
        return CreatedAtAction(nameof(GetUserById), new { id = registrationResultDto.UserInfo.Id }, registrationResultDto);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userForAuthentificationDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Username == userForAuthentificationDto.Username);

        if (user is null || !BCrypt.Verify(userForAuthentificationDto.Password, user.Password))
        {
            return BadRequest("Username or password is incorrect.");
        }

        var outputDto = new AuthenticationTokenOutputDto() { Token = authenticationManager.CreateToken(user) };

        return Ok(outputDto);
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUserInfo()
    {
        var outputModel = authenticationManager.GetCurrentUser(User);
        return Ok(outputModel);
    }
}
