using AutoMapper;
using MeetupPlatformApi.Context;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MeetupPlatformApi.Controllers
{
    [Route("/api/meetups/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public AuthenticationController(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userFromBody)
        {
            var user = mapper.Map<UserEntity>(userFromBody);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return StatusCode(201);
        }
    }
}
