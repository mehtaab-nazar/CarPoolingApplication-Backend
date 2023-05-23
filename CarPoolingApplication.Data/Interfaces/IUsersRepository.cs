using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Interfaces
{
    public interface IUsersRepository:IGenericRepository<User>
    {
        Task<User> CheckUserExists(User UserDetails);
        Task<User> CheckIdExists(string id);
    }
}
