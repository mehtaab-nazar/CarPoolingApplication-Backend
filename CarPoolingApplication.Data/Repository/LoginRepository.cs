using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarPoolingApplication.Data.Repository
{
    public class LoginRepository : GenericRepository<UserLogin>, ILoginRepository
    {
        private readonly DataContext _dataContext;
        public LoginRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ActionResult<User>> IfUserExists(UserLogin UserDetails)
        {
            var user = await _dataContext.Users.Where(user => user.EmailId == UserDetails.EmailId).FirstOrDefaultAsync();
            return user;
        }
        public async Task<ActionResult<User>> CheckIfUserExistsByEmail(GoogleJsonWebSignature.Payload payload)
        {
            var user = await _dataContext.Users.Where(user => user.EmailId == payload.Email).FirstOrDefaultAsync();
            return user;
        }

    }
    }
