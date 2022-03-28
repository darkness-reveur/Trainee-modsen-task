using AutoMapper;
using MeetupPlatformApi.Context;
using Microsoft.AspNetCore.Mvc;

namespace MeetupPlatformApi.Controllers
{
    [Route("/api/meetups")]
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


    }
}
