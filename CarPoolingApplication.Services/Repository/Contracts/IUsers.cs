
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApplication.Services.Repository.Contracts
{
    public interface IUsers
    {
        Task<IActionResult> AddUser(User UserDetails);
        Task<List<UserDTO>> GetUsers();
        Task<ActionResult<UserDTO>> GetUserDetailsById(string id);
        Task<List<OfferedRidesDTO>> GetOfferedRides(string id);
        Task<List<BookedRidesDTO>> GetBookedRides(string id);
    }
}
