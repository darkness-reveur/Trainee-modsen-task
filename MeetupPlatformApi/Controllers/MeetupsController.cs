namespace MeetupPlatformApi.Controllers;

using AutoMapper;
using MeetupPlatformApi.DataTransferObjects;
using MeetupPlatformApi.Domain;
using MeetupPlatformApi.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("/api/meetups")]
[ApiController]
public class MeetupsController : ControllerBase
{
    private readonly ApplicationContext context;
    private readonly IMapper mapper;

    public MeetupsController(ApplicationContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMeetups()
    {
        var meetups = await context.Meetups.ToListAsync();
        var outputDtos = mapper.Map<IEnumerable<MeetupOutputDto>>(meetups);
        return Ok(outputDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMeetupById([FromRoute] Guid id)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        var outputDto = mapper.Map<MeetupOutputDto>(meetup);
        return outputDto is not null ? Ok(outputDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddMeetup([FromBody] MeetupInputDto inputDto)
    {
        var meetup = mapper.Map<Meetup>(inputDto);
        await context.Meetups.AddAsync(meetup);
        await context.SaveChangesAsync();

        var outputDto = mapper.Map<MeetupOutputDto>(meetup);
        return CreatedAtAction(nameof(GetMeetupById), new { id = outputDto.Id }, outputDto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMeetup([FromRoute] Guid id, [FromBody] MeetupInputDto inputDto)
    {
        var meetup = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        mapper.Map(inputDto, meetup);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
    {
        var exMeetupEntity = await context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (exMeetupEntity is null)
        {
            return NotFound();
        }

        context.Meetups.Remove(exMeetupEntity);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
