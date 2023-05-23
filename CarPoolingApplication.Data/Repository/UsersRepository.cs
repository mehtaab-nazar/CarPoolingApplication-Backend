using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        private DataContext _dataContext;
        public UsersRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> CheckUserExists(User UserDetails)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(User => UserDetails.EmailId == User.EmailId);
        }
        public async Task<User> CheckIdExists(string id)
        {
           return await _dataContext.Users.FirstOrDefaultAsync(user => user.Id.ToString() == id || user.EmailId == id);
        }
    }
}
