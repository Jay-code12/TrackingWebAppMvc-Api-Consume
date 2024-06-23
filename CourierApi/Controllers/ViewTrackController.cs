using CourierApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetUserAsync(string track)
        {
            var users = await _dbContext.Userstr.FirstOrDefaultAsync(d => d.TrackId == track);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> ViewTracksAsync(int id)
        {
            var track = await _dbContext.TrackHistories.Where(d => d.UserId == id).ToListAsync();
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

    }
}
