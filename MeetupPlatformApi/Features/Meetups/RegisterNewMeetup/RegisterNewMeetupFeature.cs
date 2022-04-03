namespace MeetupPlatformApi.Features.Meetups.RegisterNewMeetup;

using AutoMapper;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Tags("Meetups")]
public class RegisterNewMeetupFeature : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public RegisterNewMeetupFeature(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    
    [HttpPost("/api/meetups")]
    public async Task<IActionResult> RegisterNewMeetup([FromBody] RegistrationDto registrationDto)
    {
        var meetup = mapper.Map<Meetup>(registrationDto);
        context.Meetups.Add(meetup);
        await context.SaveChangesAsync();

        var registeredMeetupDto = mapper.Map<RegisteredMeetupDto>(meetup);
        return CreatedAtRoute("GetMeetup", new { id = registeredMeetupDto.Id }, registeredMeetupDto);
    }
}
