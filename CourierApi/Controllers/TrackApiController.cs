using CourierApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrackApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TrackApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackAsync(int id)
        {
            var users = await _dbContext.TrackHistories.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTracksAsync(int id)
        {
            var track = await _dbContext.TrackHistories.Where(d => d.UserId == id).ToListAsync();
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        [HttpPost]
        public async Task<IActionResult> PostTrackAsync(TrackHistoryApi model)
        {
            if (model.Id > 0)
            {
                model.Id = 0;
            }

            await  _dbContext.TrackHistories.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditTrackAsync(TrackHistoryApi model)
        {
            if (model == null || model.Id == 0)
            {
                if (model == null)
                {
                    return BadRequest();
                }
                else if (model.Id == 0)
                {
                    return BadRequest();
                }
            }

            var user = await _dbContext.TrackHistories.FindAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Description = model.Description;
            user.Location = model.Location;

             _dbContext.TrackHistories.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackAsync(int id)
        {
            var user = await _dbContext.TrackHistories.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.TrackHistories.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindTrackAsync(string track)
        {
            var _user = await _dbContext.Userstr.FirstOrDefaultAsync(d => d.TrackId == track);

            if (_user != null)
            {
                return Ok();
            }
            return BadRequest("");
        }
    }
}
