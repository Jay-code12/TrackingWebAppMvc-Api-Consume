using CourierApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourierApi.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ViewTrackController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ViewTrackController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("{track}")]
        public IActionResult GetUser(string track)
        {
            var users = _dbContext.Userstr.FirstOrDefault(d => d.TrackId == track);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult ViewTracks(int id)
        {
            var track = _dbContext.TrackHistories.Where(d => d.UserId == id).ToList();
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

    }
}
