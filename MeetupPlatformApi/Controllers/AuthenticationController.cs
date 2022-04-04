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
        var refreshToken = new RefreshTokenEntity()
        {
            Id = authenticationManager.GetNameIdentifier(tokenPair.RefreshToken),
            Expires = authenticationManager.GetExpires(tokenPair.RefreshToken),
            UserId = user.Id
        };
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();

        var userInfoDto = mapper.Map<UserOutputDto>(user);
        var outputDto = new UserRegistrationResultDto
        {
            UserInfo = userInfoDto,
            TokenPair = new TokenPairDto
            {
                AccessToken = tokenPair.AccessToken,
                RefreshToken = tokenPair.RefreshToken
            }
        };
        return CreatedAtAction(nameof(GetUserById), new {id = outputDto.UserInfo.Id}, outputDto);
    }

    [HttpPost("refresh-tokens")]
    public async Task<IActionResult> RefreshTokenPair([FromBody] RefreshTokenDto refreshToken)
    {
        var refreshTokenId = authenticationManager.GetNameIdentifier(refreshToken.RefreshToken);
        var refreshTokenInfo = await context.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Id == refreshTokenId);

        if (refreshTokenInfo is null)
        {
            return NotFound($"Token with id: {refreshTokenId} doesn't exist.");
        }

        var user = await context.Users.Where(user => user.Id == refreshTokenInfo.UserId).SingleOrDefaultAsync();

        var tokenPair = authenticationManager.IssueTokenPair(user);
        var newRefreshToken = new RefreshTokenEntity()
        {
            Id = authenticationManager.GetNameIdentifier(tokenPair.RefreshToken),
            Expires = authenticationManager.GetExpires(tokenPair.RefreshToken),
            UserId = user.Id
        };
        context.RefreshTokens.Remove(refreshTokenInfo);
        await context.RefreshTokens.AddAsync(newRefreshToken);
        await context.SaveChangesAsync();

        var outputDto = new TokenPairDto { AccessToken = tokenPair.AccessToken, RefreshToken = tokenPair.RefreshToken };
        return Ok(outputDto);
    }

    [HttpDelete("{userId:guid}/refresh-tokens")]
    [Authorize]
    public async Task<IActionResult> RevokeUserRefreshTokens(Guid userId)
    {
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == userId);

        if (user is null)
        {
            return NotFound($"User with id: {userId} doesn't exist.");
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
        var refreshToken = new RefreshTokenEntity()
        {
            Id = authenticationManager.GetNameIdentifier(tokenPair.RefreshToken),
            Expires = authenticationManager.GetExpires(tokenPair.RefreshToken),
            UserId = user.Id
        };
        await context.RefreshTokens.AddAsync(refreshToken);
        await context.SaveChangesAsync();

        var outputDto = new TokenPairDto { AccessToken = tokenPair.AccessToken, RefreshToken = tokenPair.RefreshToken };
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

    [HttpPut("me/credentials")]
    [Authorize]
    public async Task<IActionResult> ChangeCredentials([FromBody] UserCredentialsChangeDto credentialsChangeDto)
    {
        var currentUserInfo = authenticationManager.GetCurrentUserInfo(User);
        var user = await context.Users.SingleOrDefaultAsync(user => user.Id == currentUserInfo.UserId);
        var usernameAlreadyTaken = await context.Users.AnyAsync(user => user.Username == credentialsChangeDto.Username);

        if (user is null || !BCrypt.Verify(credentialsChangeDto.OldPassword, user.Password) || usernameAlreadyTaken)
        {
            return BadRequest($"User with username: {credentialsChangeDto.Username} doesn't exist or password is incorrect.");
        }

        var userRefreshTokens = await context.RefreshTokens.Where(token => token.UserId == user.Id).ToListAsync();

        user.Password = BCrypt.HashPassword(credentialsChangeDto.NewPassword);
        user.Username = credentialsChangeDto.Username;
        context.RefreshTokens.RemoveRange(userRefreshTokens);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
