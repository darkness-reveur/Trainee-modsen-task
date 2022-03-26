using AutoMapper;
using MeetupPlatformApi.DataBase;
using MeetupPlatformApi.DataBase.Entities;
using MeetupPlatformApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.Controllers;

[Route("api/meetups/")]
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
        var meetupsEntity = await _context.Meetups.ToListAsync();

        var meetupsOutputDto = _mapper.Map<List<MeetupOutputDto>>(meetupsEntity);

        return Ok(meetupsOutputDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMeetupById(int id)
    {
        var meetupEntity = await _context.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

        var meetupOutputDto = _mapper.Map<MeetupOutputDto>(meetupEntity);

        return meetupOutputDto is not null ? Ok(meetupOutputDto) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddMeetup([FromBody] MeetupInputDto meetupInputDto)
    {
        var meetupEntity = _mapper.Map<MeetupEntity>(meetupInputDto);

        await _context.Meetups.AddAsync(meetupEntity);

        await _context.SaveChangesAsync();

        var meetupOutputDto = _mapper.Map<MeetupOutputDto>(meetupEntity);

        return CreatedAtAction(nameof(GetMeetupById), new { id = meetupOutputDto.Id }, meetupOutputDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeetup(int id, [FromBody] MeetupInputDto meetupInputDto)
    {
        var exMeetupEntity = await _context.Meetups
            .AsNoTracking()
            .FirstOrDefaultAsync(meetup => meetup.Id == id);

        if (exMeetupEntity is null)
        {
            return NotFound();
        }

        _mapper.Map(meetupInputDto, exMeetupEntity);

        _context.Meetups.Update(exMeetupEntity);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        var exMeetupEntity = await _context.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

        if (exMeetupEntity is null)
        {
            return NotFound();
        }

        _context.Meetups.Remove(exMeetupEntity);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
