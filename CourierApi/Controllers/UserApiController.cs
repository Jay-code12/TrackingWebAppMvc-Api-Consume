using CourierApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourierApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        
        private readonly ApplicationDbContext _dbContext;

        public UserApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _dbContext.Userstr.ToListAsync();
            if(users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id) 
        { 
            var user = await _dbContext.Userstr.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostUserAsync(UserApi model)
        {
            if (model.Id > 0)
            {
                model.Id = 0;
            }
            if(_dbContext.Userstr.FirstOrDefault(d => d.TrackId == model.TrackId) == null)
            {
                await _dbContext.Userstr.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> EditUserAsync(UserApi model)
        {
            if(model == null || model.Id == 0)
            {
                if(model == null) 
                { 
                    return BadRequest();
                }else if (model.Id == 0)
                {
                    return BadRequest();
                }
            }

            var user = await _dbContext.Userstr.FindAsync(model.Id);
            if(user == null)
            {
                return NotFound();
            }

            user.Description = model.Description;
            user.OriginLocation = model.OriginLocation;
            user.DestinationLocation = model.DestinationLocation;

            user.SenderName = model.SenderName;
            user.SenderContact = model.SenderContact;
            user.SenderAddress = model.SenderAddress;

            user.ReceiverAddress = model.ReceiverAddress;
            user.ReceiverContact = model.ReceiverContact;
            user.ReceiverName = model.ReceiverName;
            //user.Created = model.Created;


            _dbContext.Userstr.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Userstr.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Userstr.Remove(user);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }


    }
}
