using MeetupPlatformApi.DataBase;
using MeetupPlatformApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatformApi.Controllers
{
    [Route("api/meetups/")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private DbContextOptions<MeetupsPlatformContext> options { get; set; }

        private readonly MeetupsPlatformContext _meetupsPlatformContext;

        public MeetupsController(MeetupsPlatformContext meetupsPlatformContext)
        {
            _meetupsPlatformContext = meetupsPlatformContext;

            options = new DbContextOptionsBuilder<MeetupsPlatformContext>()
                            .UseInMemoryDatabase(databaseName: "Test")
                            .Options;
        }

        [HttpPost("test")]
        public async Task<IActionResult> AddTestMeetups()
        {
            MeetupDTO testMeetup = new MeetupDTO
            {
                Id = 0,
                Name = "testc Meetup",
                Description = "First testing meetup meetup",
                CountOfVisitors = 1,
                EndTime = DateTime.Today,
                StartTime = DateTime.Today
            };

            await _meetupsPlatformContext.Meetups.AddAsync(testMeetup);

            await _meetupsPlatformContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeetupById), new { id = testMeetup.Id }, testMeetup);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeetups()
        {
            var meetups = await _meetupsPlatformContext.Meetups.ToListAsync();

            return Ok(meetups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetupById(int id)
        {
            if (id > 0)
            {
                var meetup = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

                return meetup is not null ? Ok(meetup) : NotFound();
            }
            return BadRequest("Incorrect index");
        }

        [HttpPost]
        public async Task<IActionResult> AddMeetup([FromBody] MeetupDTO meetupDTO)
        {
            if (meetupDTO is not null)
            {
                await _meetupsPlatformContext.Meetups.AddAsync(meetupDTO);

                await _meetupsPlatformContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMeetupById), new { id = meetupDTO.Id }, meetupDTO);
            }

            return BadRequest("Empty entity");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeetup(int id, [FromBody] MeetupDTO meetupDTO)
        {
            if (meetupDTO is not null)
            {
                var exMeetup = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

                if (exMeetup is not null)
                {
                    exMeetup.Name = meetupDTO.Name;

                    exMeetup.Description = meetupDTO.Description;

                    exMeetup.StartTime = meetupDTO.StartTime;

                    exMeetup.EndTime = meetupDTO.EndTime;

                    exMeetup.CountOfVisitors = meetupDTO.CountOfVisitors;

                    await _meetupsPlatformContext.SaveChangesAsync();

                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest("Empty entity");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            if (id > 0)
            {
                var exMeetup = await _meetupsPlatformContext.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == id);

                _meetupsPlatformContext.Meetups.Remove(exMeetup);

                await _meetupsPlatformContext.SaveChangesAsync();

                return NoContent();
            }

            return BadRequest();
        }
    }
}
