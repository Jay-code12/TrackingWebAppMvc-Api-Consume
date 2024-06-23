using CourierApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetTrack(int id)
        {
            var users = _dbContext.TrackHistories.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetTracks(int id)
        {
            var track = _dbContext.TrackHistories.Where(d => d.UserId == id).ToList();
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        [HttpPost]
        public IActionResult PostTrack(TrackHistoryApi model)
        {
            if (model.Id > 0)
            {
                model.Id = 0;
            }

            _dbContext.TrackHistories.Add(model);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult EditTrack(TrackHistoryApi model)
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

            var user = _dbContext.TrackHistories.Find(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Description = model.Description;
            user.Location = model.Location;

            _dbContext.TrackHistories.Update(user);
            _dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTrack(int id)
        {
            var user = _dbContext.TrackHistories.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.TrackHistories.Remove(user);
            _dbContext.SaveChanges();
            return Ok();

        }

        [HttpGet("{id}")]
        public IActionResult FindTrack(string track)
        {
            var _user = _dbContext.Userstr.FirstOrDefault(d => d.TrackId == track);

            if (_user != null)
            {
                return Ok();
            }
            return BadRequest("");
        }
    }
}
