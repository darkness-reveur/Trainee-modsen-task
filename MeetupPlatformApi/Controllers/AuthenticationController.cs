using AutoMapper;
using MeetupPlatformApi.Authentification;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userFromBody)
        {
            var user = mapper.Map<UserEntity>(userFromBody);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return StatusCode(201);
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
    }
}
