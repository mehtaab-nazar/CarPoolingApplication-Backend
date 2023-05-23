using AutoMapper;
using CarPoolingApplication.Data;
using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Data.Repository;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.Models;
using CarPoolingApplication.Models.ViewModels;
using CarPoolingApplication.Services.CustomExceptions;
using CarPoolingApplication.Services.Repository.Contracts;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarPoolingApplication.Services.Repository.Services
{
    public class LoginService :ILogin
    {
        private readonly ILoginRepository _dataContext;
        private readonly IConfiguration _config;
        private readonly IUsersRepository _usersData;
        private readonly IMapper _mapper;

        public LoginService(ILoginRepository dataContext, IConfiguration config,IUsersRepository usersData,IMapper mapper)
        {
            _dataContext = dataContext;
            _config = config;
            _usersData = usersData;
            _mapper = mapper;
        }
        public async Task<ActionResult> LoginUser(UserLogin userDetails)
        {
            var user = await _dataContext.IfUserExists(userDetails);

            if (user == null)
            {
                throw new UserNotFoundException("User isnt found");
            }
            else if (user.Value.Password == userDetails.Password)
            {
                var token = GenerateToken(user.Value.EmailId);
                return new OkObjectResult(new
                {
                    statuscode = 200,
                    JwtToken = token
                });
            }
            else
            {
                return new BadRequestObjectResult("Please enter correct password");
            }
        }

        public string GenerateToken(string UserEmail)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,UserEmail),
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
        public async Task<ActionResult> LoginUserWithGoogle(string credential)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(credential);
            var user = await _dataContext.CheckIfUserExistsByEmail(payload);
            if (user.Value != null)
            {
                var token = GenerateToken(payload.Email);
                return new OkObjectResult(new
                {
                    statuscode = 200,
                    JwtToken = token
                });
            }
            else
            {
                UserDTO newUser = new UserDTO();
                newUser.EmailId = payload.Email;
                newUser.FirstName = payload.GivenName;
                newUser.LastName = payload.FamilyName;
                newUser.Password = payload.GivenName+"@123";
                newUser.ProfileImage = payload.Picture;
                newUser.UserId = payload.GivenName+"_"+payload.FamilyName;
                _usersData.AddData(_mapper.Map<User>(newUser));
                var token = GenerateToken(payload.Email);
                return new OkObjectResult(new
                {
                    statuscode = 200,
                    JwtToken = token
                });
            }
        } 
    }
}
