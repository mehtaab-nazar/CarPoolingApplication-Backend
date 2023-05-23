
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApplication.Services.Repository.Contracts
{
    public interface IOfferedRides
    {
        Task<List<OfferedRidesDTO>> GetOfferedRides();
        Task<IActionResult> AddRide(OfferedRides ride);
        Task<OfferedRidesDTO> GetRideById(int id);
        //Task<ActionResult<OfferedRides>> UpdateRide(OfferedRides ride, int id);
        //Task<ActionResult<OfferedRides>> DeleteRide(int id);
    }
}
