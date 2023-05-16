using CarPoolingApplication.Data;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.Models;
using CarPoolingApplication.Services.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace CarPoolingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly DataContext _data;
        public LoginController(IConfiguration config, DataContext data)
        {
            _config = config;
            _data = data;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<String>> GetClaims()
        {
            var claimId = User.Claims.FirstOrDefault(X=> X.Type.Equals(ClaimTypes.Email)).Value;
            return Ok(claimId);
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser([FromBody]UserLogin UserDetails)
        {
            var user =await _data.Users.Where(user => user.EmailId == UserDetails.EmailId).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserNotFoundException("User isnt found");
            }
            else if(user.Password == UserDetails.Password)
            {
                var token = GenerateToken(user);
                return Ok(new
                {
                    StatusCode = 200,
                    JwtToken = token
                });
            }
            else
            {
                return BadRequest("Please enter correct password");
            }
        }
        private string GenerateToken(User User)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,User.EmailId),
            };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credential
                );
            return tokenhandler.WriteToken(token);
        }
    }

}
