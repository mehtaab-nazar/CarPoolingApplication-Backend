using CarPoolingApplication.Models;
using CarPoolingApplication.Models.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Interfaces
{
    public interface ILoginRepository:IGenericRepository<UserLogin>
    {
       Task<ActionResult<User>> IfUserExists(UserLogin userDetails);
        Task<ActionResult<User>> CheckIfUserExistsByEmail(GoogleJsonWebSignature.Payload payload);
    }
}
