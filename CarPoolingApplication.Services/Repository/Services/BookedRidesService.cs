
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarPoolingApplication.Services.Repository.Contracts;
using CarPoolingApplication.Data;
using CarPoolingApplication.Models;
using CarPoolingApplication.Services.CustomExceptions;
using CarPoolingApplication.Data.Repository;
using System.Collections;
using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Models.ViewModels;
using AutoMapper;

namespace CarPoolingApplication.Services.Repository.Services
{
    public class BookedRidesService : IBookedRides
    {
        private readonly IBookedRidesRepository _bookedRidesData;
        private readonly IOfferedRidesRepository _offeredRidesData;
        private readonly IMapper _mapper;
        public BookedRidesService(IBookedRidesRepository bookedRidesData,IOfferedRidesRepository offeredRidesData,IMapper mapper)
        {
            _offeredRidesData = offeredRidesData;
            _bookedRidesData = bookedRidesData;
            _mapper = mapper;
        }
        public async Task<List<BookedRidesDTO>> GetBookedRides()
        {
            var data =await _bookedRidesData.GetData();
            List<BookedRidesDTO> rides = new List<BookedRidesDTO>();
  
            if (data.Count== 0)
            {
                throw new RideNotFoundException("There are no booked rides");
            }
            else
            {
                foreach(var ride in data)
                {
                    rides.Add(_mapper.Map<BookedRidesDTO>(ride));
                }
                return rides;
            }
        }
        public async Task<IActionResult> AddRide(BookedRides ride)
        {
            if (ride.Cost > 0)
            {
                var OfferedRide = await _offeredRidesData.GetDataById(ride.OfferedId);
                OfferedRide.AvailableSeats = OfferedRide.AvailableSeats - ride.BookedSeats;
                ride.SeatsLeft = OfferedRide.AvailableSeats - ride.BookedSeats;
                _bookedRidesData.AddData(ride);

                return new OkResult();
            }
            else if (ride.Cost == 0)
            {
                throw new RideNotBookedException("The cost is 0");
            }
            else
            {
                throw new RideNotBookedException("The cost is less than zero");
            }
        }
        public async Task<ArrayList> GetMatchingRides(BookedRidesDTO ride)
        {
            var offeredRidesList = new ArrayList();
            var offeredRides = await _offeredRidesData.GetData();
            var bookedRides = await _bookedRidesData.GetData();

            if (offeredRides == null)
            {
                throw new RideNotFoundException($"Rides do not exist");
            }
            else
            {
                foreach (var rideDetails in offeredRides)
                {
                    bool startCheck = false;
                    bool endCheck = false;
                    ICollection<string> stops = rideDetails.Stops;

                    if (stops.Count == 0 && ride.StartingLocation == rideDetails.StartingLocation && ride.EndingLocation == rideDetails.EndingLocation)
                    { /*Count of stops is zero*/
                        startCheck = true; endCheck = true;
                    }

                    foreach (var stop in stops)
                    {/*stops exist*/
                        if (stop == ride.StartingLocation || ride.StartingLocation == rideDetails.StartingLocation)
                        {
                            startCheck = true;
                        }

                        if (stop == ride.EndingLocation || ride.EndingLocation == rideDetails.EndingLocation)
                        {
                            endCheck = true;
                        }
                    }

                    if (startCheck == true && endCheck == true && rideDetails.Id != ride.Id)
                    {
                        if (rideDetails.Date == ride.Date && rideDetails.AvailableSeats >= ride.BookedSeats)
                        {
                            offeredRidesList.Add(rideDetails);
                        }
                    }
                }

                foreach (var bookedRide in bookedRides)
                {
                    if (bookedRide.EndingLocation == ride.StartingLocation && bookedRide.BookedSeats >= ride.BookedSeats && bookedRide.Date == ride.Date)
                    {
                        var id = bookedRide.OfferedId;
                        var offeredRide = await _offeredRidesData.GetDataById(id);
                        var previous = offeredRidesList.Contains(offeredRide.OfferedId);
                        if (offeredRide.Id != ride.Id) { 
                        if (previous == true)
                        {
                            offeredRide.AvailableSeats = offeredRide.AvailableSeats + ride.BookedSeats;
                            offeredRidesList.Add(offeredRide);
                        }
                        else
                        {
                            offeredRidesList.Add(offeredRide);
                        }
                        }
                    }
                }
            }
            return offeredRidesList;
        }
        public async Task<ActionResult<BookedRidesDTO>> GetRideById(int id)
        {
            var bookedRide = await _bookedRidesData.GetDataById(id);

            if (bookedRide == null)
            {
                throw new RideNotFoundException($"Ride with id-{id} doesnt exist");
                
            }
            else
            {
                return _mapper.Map<BookedRidesDTO>(bookedRide);
            }
        }
       /* public async Task<ActionResult<BookedRides>> UpdateRide(BookedRides ride, int id)
        {
            var bookedRide = await _bookedRidesData.GetDataById(id);

            if (bookedRide == null)
            {
                throw new RideNotFoundException($"Ride with id-{id} doesnt exist");
            }
            else
            {
                bookedRide.StartingLocation = ride.StartingLocation;
                bookedRide.EndingLocation = ride.EndingLocation;
                bookedRide.TimeSlot = ride.TimeSlot;

                return bookedRide;
            }
        }
        public async Task<ActionResult<BookedRides>> DeleteRide(int id)
        {
            var bookedRide = await _dataContext.BookedRides.FindAsync(id);

            if (bookedRide == null)
            {
                throw new RideNotFoundException($"Ride with id-{id} doesnt exist");
            }
            else
            {
                bookedRide.IsActive = false;

                return bookedRide;
            }
        }*/
    }
}
