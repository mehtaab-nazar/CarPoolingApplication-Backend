
using AutoMapper;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
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

        private readonly IMapper _mapper;

        public OfferedRidesController(IOfferedRides dataContext,ILogger<OfferedRidesController> logger,IMapper mapper)
        {
            _dataContext = dataContext;

            _logger = logger;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferedRidesDTO>>> FetchOfferedRides()
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
        public async Task<ActionResult<OfferedRidesDTO>> AddRide([FromBody] OfferedRidesDTO ride)
        {
            try
            {
                var data = await _dataContext.AddRide(_mapper.Map<OfferedRides>(ride));

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
        public async Task<ActionResult<OfferedRidesDTO>> FetchRideById([FromRoute] int offeringId)
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

      /*  [HttpPut("{offeringId}")]
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
        }*/
    }
}