using AutoMapper;
using MeetupPlatformApi.DataBase;
using MeetupPlatformApi.DataBase.Entities;
using MeetupPlatformApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.Controllers;

[Route("/api/meetups")]
[ApiController]
public class MeetupsController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public MeetupsController(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMeetups()
    {
        var meetups = await _context.Meetups.ToListAsync();
        var outputDtos = _mapper.Map<IEnumerable<MeetupOutputDto>>(meetups);
        return Ok(outputDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMeetupById([FromRoute] int id)
    {
        var meetup = await _context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        var outputDto = _mapper.Map<MeetupOutputDto>(meetup);
        return outputDto is not null ? Ok(outputDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddMeetup([FromBody] MeetupInputDto inputDto)
    {
        var meetup = _mapper.Map<MeetupEntity>(inputDto);
        await _context.Meetups.AddAsync(meetup);
        await _context.SaveChangesAsync();

        var outputDto = _mapper.Map<MeetupOutputDto>(meetup);
        return CreatedAtAction(nameof(GetMeetupById), new { id = outputDto.Id }, outputDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMeetup([FromRoute] int id, [FromBody] MeetupInputDto inputDto)
    {
        var meetup = await _context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (meetup is null)
        {
            return NotFound();
        }

        _mapper.Map(inputDto, meetup);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] int id)
    {
        var exMeetupEntity = await _context.Meetups.SingleOrDefaultAsync(meetup => meetup.Id == id);
        if (exMeetupEntity is null)
        {
            return NotFound();
        }

        _context.Meetups.Remove(exMeetupEntity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
