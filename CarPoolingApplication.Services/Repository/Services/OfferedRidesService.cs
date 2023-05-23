
using CarPoolingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarPoolingApplication.Services.Repository.Contracts;
using CarPoolingApplication.Data;
using CarPoolingApplication.Services.CustomExceptions;
using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Models.ViewModels;
using AutoMapper;

namespace CarPoolingApplication.Services.Repository.Services
{
    public class OfferedRidesService : IOfferedRides
    {
        private readonly IOfferedRidesRepository _dataContext;
        private readonly IMapper _mapper;
        public OfferedRidesService(IOfferedRidesRepository dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<List<OfferedRidesDTO>> GetOfferedRides()
        {
            var data = await _dataContext.GetData();
            List<OfferedRidesDTO> rides = new List<OfferedRidesDTO>();

            if (data.Count == 0)
            {
                throw new RideNotFoundException("There are no offered rides");
            }
            else
            {
                foreach(var ride in data)
                {
                    rides.Add(_mapper.Map<OfferedRidesDTO>(ride));
                }
                return rides;
            }
        }
        public async Task<IActionResult> AddRide(OfferedRides ride)
        {
            if (ride.Cost > 0)
            {
                ride.AvailableSeats = ride.SeatsOffered;
                _dataContext.AddData(ride);

                return new OkResult();
            }
            else if (ride.Cost == 0)
            {
                throw new RideNotOfferedException("The cost of the ride is 0");
            }
            else
            {
                throw new RideNotOfferedException("The cost is less than zero");
            }
        }
        public async Task<OfferedRidesDTO> GetRideById(int id)
        {
            var offeredRide = await _dataContext.GetDataById(id);

            if (offeredRide != null)
            {
                return _mapper.Map<OfferedRidesDTO>(offeredRide);
            }
            else
            {
                throw new RideNotFoundException($"Ride with id-{id} doesnt exist");
            }
        }
       /* public async Task<ActionResult<OfferedRides>> UpdateRide(OfferedRides request, int id)
        {
            var offeredRide = await _dataContext.GetDataById(id);

            if (offeredRide == null)
            {
                throw new RideNotFoundException($"Ride with id-{id} doesnt exist");
            }
            else
            {
                offeredRide.StartingLocation = request.StartingLocation;
                offeredRide.EndingLocation = request.EndingLocation;
                offeredRide.Stops = request.Stops;
                offeredRide.Cost = request.Cost;
                offeredRide.AvailableSeats = request.AvailableSeats;
                offeredRide.TimeSlot = request.TimeSlot;

                return offeredRide;
            }
        }
        public async Task<ActionResult<OfferedRides>> DeleteRide(int id)
        {
            var offeredRide = await _data.OfferedRides.FindAsync(id);

            if (offeredRide == null)
            {
                throw new RideNotFoundException($"Ride with id-{id} doesnt exist");
            }
            else
            {
                offeredRide.IsActive = false;
                _data.SaveChanges();

                return offeredRide;
            }
        }*/
    }
}
