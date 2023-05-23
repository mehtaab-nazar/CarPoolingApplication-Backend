using CarPoolingApplication.Data;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.Models;
using CarPoolingApplication.Services.CustomExceptions;
using CarPoolingApplication.Services.Repository.Contracts;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarPoolingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {

        private readonly ILogin _data;
        public LoginController( ILogin data)
        {
            _data = data;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<String>> GetClaims()
        {
            var claimId = User.Claims.FirstOrDefault(X => X.Type.Equals(ClaimTypes.Email)).Value;
            return Ok(claimId);
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser([FromBody]UserLogin UserDetails)
        {
            var data = await _data.LoginUser(UserDetails);
            return data;
        }
        [HttpPost("LoginWithGoogle")]
        public async Task<ActionResult> LoginUserWithGoogle([FromBody] string credential)
        {
            var data = await _data.LoginUserWithGoogle(credential);
            return data;
        }
    }

}
