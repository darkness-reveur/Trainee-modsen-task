﻿namespace MeetupPlatformApi.Controllers;

using AutoMapper;
using MeetupPlatformApi.Authentification;
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
        var userFromDb = await context.Users.SingleOrDefaultAsync(u => u.Id.Equals(id));
        var userOutput = mapper.Map<UserOutputDto>(userFromDb);

        return userOutput == null ? Ok(userOutput) : NotFound();
    }

    [HttpPost("authentication")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userFromBody)
    {
        var user = mapper.Map<UserEntity>(userFromBody);

        user.Password = BCrypt.HashPassword(user.Password);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var userOutput = mapper.Map<UserOutputDto>(user);

        return CreatedAtAction(nameof(GetUserById), new { id = userOutput.Id }, userOutput);
    }

    [HttpPost("authentication/login")]
    public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationDto userForAuthentificationDto)
    {
        var userFromDb = await context.Users.SingleOrDefaultAsync(u => u.Username.Equals(userForAuthentificationDto.Username));

        if (userFromDb == null || !BCrypt.Verify(userForAuthentificationDto.Password, userFromDb.Password))
        {
            return Unauthorized();
        }

        return Ok(new { Token = authenticationManager.CreateToken(userFromDb) });
    }

    [HttpGet("user")]
    [Authorize]
    public IActionResult GetCurrentUserInfo()
    {
        var userNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        return Ok(new { Id = userNameIdentifier });
    }
}
