using Azure;
using CourierApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CourierApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(IConfiguration config, ApplicationDbContext dbContext, 
                                SignInManager<IdentityUser> signInManager, 
                                UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
;
        }

        private AdminLogin AuthenticateUser(AdminLogin user)
        {
            var _user =  _dbContext.AdminLogin.FirstOrDefault(d => d.UserName == user.UserName && d.Password == user.Password);

            if (_user == null)
            {
                _ = _user == null;
            }
            return _user;
        }

        private string GenerateToken(AdminLogin users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"], 
                null, 
                expires: DateTime.Now.AddMinutes(1), 
                signingCredentials: credentials );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(AdminLogin user)
        {
            var user_ =  AuthenticateUser(user);
            if(user_ != null) 
            { 
                var token = GenerateToken(user_);
                return Ok(new { token = token });
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProfileAsync(int id)
        {
            var users = await _dbContext.AdminLogin.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

    }
}
