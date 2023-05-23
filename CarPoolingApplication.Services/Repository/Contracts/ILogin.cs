using CarPoolingApplication.Models;
using CarPoolingApplication.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Services.Repository.Contracts
{
    public interface ILogin
    {
         Task<ActionResult> LoginUser(UserLogin userDetails);
         string GenerateToken(string UserEmail);

        Task<ActionResult> LoginUserWithGoogle(string credential);
    }
}
