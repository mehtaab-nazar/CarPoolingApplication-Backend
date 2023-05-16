
using CarPoolingApplication.Models;
using CarPoolingApplication.Services.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferedRidesController : ControllerBase
    {
        private readonly ILogger<OfferedRidesController> _logger;

        private readonly IOfferedRides _dataContext;

        public OfferedRidesController(IOfferedRides dataContext,ILogger<OfferedRidesController> logger)
        {
            _dataContext = dataContext;

            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferedRides>>> GetOfferedRides()
        {
            try
            {
                var data = await _dataContext.GetOfferedRides();

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<OfferedRides>> AddRide([FromBody] OfferedRides ride)
        {
            try
            {
                var data = await _dataContext.AddRide(ride);

                if (data == null)
                {
                    _logger.LogError("Rides cannot be added");
                }

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpGet("{offeringId}")]
        public async Task<ActionResult<OfferedRides>> GetRideById([FromRoute] int offeringId)
        {
            try
            {
                var data =await _dataContext.GetRideById(offeringId);

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpPut("{offeringId}")]
        public async Task<ActionResult<OfferedRides>> UpdateRide([FromBody] OfferedRides request, [FromRoute] int offeringId)
        {
            try
            {
                var data = await _dataContext.UpdateRide(request, offeringId);

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpDelete("{offeringId}")]
        public async Task<ActionResult<OfferedRides>> DeleteRide([FromRoute] int offeringId)
        {
            try
            {
                var data = await _dataContext.DeleteRide(offeringId);

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