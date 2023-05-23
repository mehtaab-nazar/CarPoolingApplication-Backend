
using AutoMapper;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using CarPoolingApplication.Services.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers _data;

        private readonly ILogger<UsersController> _logger;

        private readonly IMapper _mapper;

        public UsersController(IUsers data, ILogger<UsersController> logger,IMapper mapper)
        {
            _data = data;

            _logger = logger;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> FetchAllUsers()
        {
            try
            {
                var users = await _data.GetUsers();
                return Ok(users);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserDTO userDetails)
        {
            try
            {
                var data = await _data.AddUser(_mapper.Map<User>(userDetails));

                if (data == null) 
                {
                    _logger.LogError("User cannot be added");
                }

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }  
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> FetchUserById([FromRoute] string id)
        {
            try
            {
                var data = await _data.GetUserDetailsById(id);

                return data;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        /*[HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User request,[FromRoute] int id)
        {
            try
            {
                var data = await _data.UpdateUser(request, id);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser([FromRoute] int id)
        {  
            try
            {
                var data = await _data.DeleteUser(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }*/

        [HttpGet("offered/{id}")]
        public async Task<ActionResult<List<OfferedRidesDTO>>> FetchOfferedRides([FromRoute] string id)
        {
            try
            {
                var offeredRides = await _data.GetOfferedRides(id);

                return Ok(offeredRides);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpGet("booked/{id}")]
        public async Task<ActionResult<List<BookedRidesDTO>>> FetchBookedRides([FromRoute] string id)
        {
            try
            {
                var data = await _data.GetBookedRides(id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }
    }
}