using AutoMapper;
using MeetupPlatformApi.DataBase;
using MeetupPlatformApi.DTO;
using MeetupPlatformApi.Entities;
using Microsoft.AspNetCore.Http;
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
            var meetups = await _meetupsPlatformContext.Meetups.ToListAsync();

            var meetupsView = _mapper.Map<List<MeetupViewModel>>(meetups);

            return Ok(meetupsView);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            var meetup = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

            var meetupView = _mapper.Map<MeetupViewModel>(meetup);

            return meetupView is not null ? Ok(meetupView) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeetup([FromBody] MeetupViewModel meetupView)
        {
            var meetupDTO = _mapper.Map<MeetupDTO>(meetupView);

            await _meetupsPlatformContext.Meetups.AddAsync(meetupDTO);

            await _meetupsPlatformContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeetupById), new { id = meetupDTO.Id }, meetupView);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetup(int id, [FromBody] MeetupViewModel meetupView)
        {
            var exMeetup = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

            var meetupDTO = _mapper.Map<MeetupDTO>(meetupView);

            if (exMeetup is not null)
            {
                exMeetup.Name = meetupDTO.Name;

                exMeetup.Description = meetupDTO.Description;

                exMeetup.StartTime = meetupDTO.StartTime;

                exMeetup.EndTime = meetupDTO.EndTime;

                await _meetupsPlatformContext.SaveChangesAsync();

                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserById([FromQuery] int id)
        {
            var exMeetup = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

            if (exMeetup is not null)
            {
                _meetupsPlatformContext.Meetups.Remove(exMeetup);

                await _meetupsPlatformContext.SaveChangesAsync();

                return NoContent();
            }
            return NotFound();
        }
    }
}
