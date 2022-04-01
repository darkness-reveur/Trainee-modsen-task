namespace MeetupPlatformApi.Controllers;

using AutoMapper;
using MeetupPlatformApi.Authentication;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        var user = mapper.Map<UserEntity>(registrationDto);

        user.Password = BCrypt.HashPassword(user.Password);

        await context.Users.AddAsync(user);

        var tokenPair = authenticationManager.IssueTokenPair(user);

        await context.RefreshTokens.AddAsync(tokenPair.RefreshToken);
        await context.SaveChangesAsync();

        var userInfoDto = mapper.Map<UserOutputDto>(user);
        var outputDto = new UserRegistrationResultDto
        {
            UserInfo = userInfoDto,
            TokenPair = new() { AccessToken = tokenPair.AccessToken, RefreshTokenId = tokenPair.RefreshToken.Id }
        };
        return CreatedAtAction(nameof(GetUserById), new {id = outputDto.UserInfo.Id}, outputDto);
    }

    [HttpPost("refreshTokens/{id:guid}")]
    public async Task<IActionResult> TokenRefresh(Guid id)
    {
        var refreshToken = await context.RefreshTokens.Where(refreshToken => refreshToken.Id == id).SingleOrDefaultAsync();

        if(refreshToken is null)
        {
            return BadRequest($"Token with id: {id} doesn't exist.");
        }

        var user = await context.Users.Where(user => user.Id == refreshToken.UserId).SingleOrDefaultAsync();

        var tokenPair = authenticationManager.IssueTokenPair(user);

        context.RefreshTokens.Remove(refreshToken);
        await context.RefreshTokens.AddAsync(tokenPair.RefreshToken);
        await context.SaveChangesAsync();

        var outputDto = new AuthenticationTokenPairOutputDto { AccessToken = tokenPair.AccessToken, RefreshTokenId = tokenPair.RefreshToken.Id };
        return Ok(outputDto);
    }

    [HttpDelete("{userId:guid}/refreshTokens")]
    public async Task<IActionResult> RevokeAllRefreshTokens(Guid userId)
    {
        var user = await context.Users.Where(user => user.Id == userId).SingleOrDefaultAsync();

        if (user is null)
        {
            return BadRequest($"User with id: {userId} doesn't exist.");
        }

        var userRefreshTokens = await context.RefreshTokens.Where(token => token.UserId == userId).ToListAsync();

        context.RefreshTokens.RemoveRange(userRefreshTokens);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto authenticationDto)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Username == authenticationDto.Username);
        if (user is null || !BCrypt.Verify(authenticationDto.Password, user.Password))
        {
            return BadRequest("Username or password is incorrect.");
        }

        var tokenPair = authenticationManager.IssueTokenPair(user);

        await context.RefreshTokens.AddAsync(tokenPair.RefreshToken);
        await context.SaveChangesAsync();

        var outputDto = new AuthenticationTokenPairOutputDto { AccessToken = tokenPair.AccessToken, RefreshTokenId = tokenPair.RefreshToken.Id };
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
