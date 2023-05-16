
using CarPoolingApplication.Models;
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

        public UsersController(IUsers data, ILogger<UsersController> logger)
        {
            _data = data;

            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
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
        public async Task<ActionResult> AddUser([FromBody] User userDetails)
        {
            try
            {
                var data = await _data.AddUser(userDetails);

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
        public async Task<ActionResult<User>> GetUserById([FromRoute] string id)
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

        [HttpPut("{id}")]
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
        }

        [HttpGet("offered/{id}")]
        public async Task<ActionResult<List<BookedRides>>> GetOfferedRides([FromRoute] string id)
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
        public async Task<ActionResult<List<OfferedRides>>> GetBookedRides([FromRoute] string id)
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