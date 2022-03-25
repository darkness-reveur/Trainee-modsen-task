using AutoMapper;
using MeetupPlatformApi.DataBase;
using MeetupPlatformApi.Dto;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.Controllers
{
    [Route("api/meetups/")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly MeetupsPlatformContext _meetupsPlatformContext;

        private readonly IMapper _mapper;

        public MeetupsController(
            MeetupsPlatformContext meetupsPlatformContext,
            IMapper mapper)
        {
            _mapper = mapper;
            _meetupsPlatformContext = meetupsPlatformContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeetups()
        {
            var meetupsEntity = await _meetupsPlatformContext.Meetups.ToListAsync();

            var meetupsOutputDto = _mapper.Map<List<MeetupOutputDto>>(meetupsEntity);

            return Ok(meetupsOutputDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            var meetupEntity = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

            var meetupOutputDto = _mapper.Map<MeetupOutputDto>(meetupEntity);

            return meetupOutputDto is not null ? Ok(meetupOutputDto) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeetup([FromBody] MeetupInputDto meetupInputDto)
        {
            var meetupEntity = _mapper.Map<MeetupEntity>(meetupInputDto);

            await _meetupsPlatformContext.Meetups.AddAsync(meetupEntity);

            await _meetupsPlatformContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeetupById), new { id = meetupEntity.Id }, meetupEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetup(int id, [FromBody] MeetupInputDto meetupInputDto)
        {
            var exMeetupEntity = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

            if (exMeetupEntity is null)
            {
                return NotFound();
            }

            exMeetupEntity.Name = meetupInputDto.Name;

            exMeetupEntity.Description = meetupInputDto.Description;

            exMeetupEntity.StartTime = meetupInputDto.StartTime;

            exMeetupEntity.EndTime = meetupInputDto.EndTime;

            await _meetupsPlatformContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var exMeetupEntity = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

            if (exMeetupEntity is null)
            {
                return NotFound();
            }

            _meetupsPlatformContext.Meetups.Remove(exMeetupEntity);

            await _meetupsPlatformContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
