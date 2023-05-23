
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CarPoolingApplication.Services.Repository.Contracts
{
    public interface IBookedRides
    {
        Task<List<BookedRidesDTO>> GetBookedRides();
        Task<ArrayList> GetMatchingRides(BookedRidesDTO ride);
        Task<ActionResult<BookedRidesDTO>> GetRideById(int id);
        //Task<ActionResult<BookedRides>> UpdateRide(BookedRides ride, int id);
        //Task<ActionResult<BookedRides>> DeleteRide(int id);
        Task<IActionResult> AddRide(BookedRides ride);
    }
}
