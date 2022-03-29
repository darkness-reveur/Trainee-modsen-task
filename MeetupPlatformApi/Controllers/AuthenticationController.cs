using AutoMapper;
using MeetupPlatformApi.Authentification;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MeetupPlatformApi.Controllers
{
    [Route("/api/meetups/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        private readonly AuthentificationManager authentificationManager;

        public AuthenticationController(ApplicationContext context, IMapper mapper, AuthentificationManager authentificationManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.authentificationManager = authentificationManager;
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id.Equals(id));
            var userOutput = mapper.Map<UserOutputDto>(user);

            return userOutput == null ? Ok(userOutput) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userFromBody)
        {
            var user = mapper.Map<UserEntity>(userFromBody);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return CreatedAtRoute("UserById", new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthentificationDto userForAuthentificationDto)
        {
            if(!await authentificationManager.ValidateUser(userForAuthentificationDto))
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
}
