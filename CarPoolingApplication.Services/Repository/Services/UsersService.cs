using AutoMapper;
using CarPoolingApplication.Data;
using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Data.Repository;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using CarPoolingApplication.Services.CustomExceptions;
using CarPoolingApplication.Services.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingApplication.Services.Repository.Services
{
    public class UsersService : IUsers
    {
        private readonly IUsersRepository _dataContext;
        private readonly IOfferedRidesRepository _offeredRidesData;
        private readonly IBookedRidesRepository _bookedRidesData;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository dataContext,IOfferedRidesRepository offeredRidesData,IBookedRidesRepository bookedRidesData,IMapper mapper)
        {
            _dataContext = dataContext;
            _offeredRidesData = offeredRidesData;
            _bookedRidesData = bookedRidesData;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> GetUsers()
        {
            var data= await _dataContext.GetData();
            var users = new List<UserDTO>();
            foreach (var user in data)
            {
                var userDTO =_mapper.Map<UserDTO>(user);
                users.Add(userDTO);
            }
            return users;
        }
        public async Task<IActionResult> AddUser(User UserDetails)
        {
            var data = await _dataContext.CheckUserExists(UserDetails);

            if (data == null)
            {
                _dataContext.AddData(UserDetails);

                return new OkResult();
            }
            else
            {
                throw new UserExistsException($"User with this email Id already exists!");
            }
        }
        public async Task<ActionResult<UserDTO>> GetUserDetailsById(string id)
        {
            var userDetails = await _dataContext.CheckIdExists(id);

            if (userDetails != null)
            {
                return _mapper.Map<UserDTO>(userDetails);
            }
            else
            {
                throw new UserNotFoundException($"User with id- {id} not found");
            }
        }
        /*public async Task<ActionResult<User>> UpdateUser(User request, int id)
        {
            var userDetails =  _dataContext.GetDataById(id);

            if (userDetails == null)
            {
                throw new UserNotFoundException($"User with id-{id} not found");
            }
            else
            {
                return _dataContext.UpdateData(request, id);
            }
        }*/
       /* public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var userDetails = _dataContext.

            if (userDetails == null)
            {
                throw new UserNotFoundException($"User with id- {id} not found");
            }
            else
            {
                userDetails.IsActive = false;
                await _dataContext.SaveChangesAsync();

                return userDetails;
            }
        }*/
        public async Task<List<OfferedRidesDTO>> GetOfferedRides(string id)
        {
            List<OfferedRidesDTO> rides = new List<OfferedRidesDTO>();
            List<OfferedRides> data = await _offeredRidesData.GetRidesOfferedByUser(id);
            
            foreach (var ride in data)
            {
                var bookedRides = await _bookedRidesData.GetRidesBookedByOfferedId(ride);
                foreach(var rideData in bookedRides)
                {
                    ride.Id = rideData.Id;
                    ride.SeatsOffered = rideData.BookedSeats;
                    rides.Add(_mapper.Map<OfferedRidesDTO>(ride));
                }
                
            }

            return data == null ? throw new RideNotFoundException("There are no offered rides") : rides;
        }
        public async Task<List<BookedRidesDTO>> GetBookedRides(string id)
        {
            var data = await _bookedRidesData.GetRidesBookedByUserId(id);
            List<BookedRidesDTO> rides = new List<BookedRidesDTO>();

            foreach (var ride in data)
            {
                var offeredRide = await _offeredRidesData.GetDataById(ride.OfferedId);
                if (offeredRide != null)
                {
                    ride.Id = offeredRide.Id;
                    rides.Add(_mapper.Map<BookedRidesDTO>(ride));
                }
            }

            return data == null ? throw new RideNotFoundException("There are no booked rides") : rides;
        }
    }
}
