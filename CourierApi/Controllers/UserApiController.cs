using CourierApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetUsers()
        {
            var users = _dbContext.Userstr.ToList();
            if(users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id) 
        { 
            var user = _dbContext.Userstr.Find(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult PostUser(UserApi model)
        {
            if (model.Id > 0)
            {
                model.Id = 0;
            }
            if(_dbContext.Userstr.FirstOrDefault(d => d.TrackId == model.TrackId) == null)
            {
                _dbContext.Userstr.Add(model);
                _dbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult EditUser(UserApi model)
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

            var user = _dbContext.Userstr.Find(model.Id);
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
            _dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Userstr.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Userstr.Remove(user);
            _dbContext.SaveChanges();
            return Ok();

        }


    }
}
