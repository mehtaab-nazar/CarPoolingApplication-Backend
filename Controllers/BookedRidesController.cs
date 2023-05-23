
using AutoMapper;
using CarPoolingApplication.Models;
using CarPoolingApplication.Models.ViewModels;
using CarPoolingApplication.Services.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookedRidesController : ControllerBase
    {
        private readonly IBookedRides _dataContext;

        private readonly ILogger<BookedRidesController> _logger;

        private readonly IMapper _mapper;

        public BookedRidesController(IBookedRides dataContext,ILogger<BookedRidesController> logger,IMapper mapper)
        {
            _dataContext = dataContext;

            _logger = logger;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookedRidesDTO>>> FetchBookedRides()
        {
            try
            {
                var users = await _dataContext.GetBookedRides();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());

                return null;
            }
        }

        [HttpPost("GetDetails")]
        public async Task<ActionResult<OfferedRidesDTO>> FetchMatchingRides([FromBody] BookedRidesDTO ride)
        { 
            try
            {
                var data = await _dataContext.GetMatchingRides(ride);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookedRidesDTO>> AddRide([FromBody] BookedRidesDTO ride)
        {
           try
           {
             var data = await _dataContext.AddRide(_mapper.Map<BookedRides>(ride));

              if (data == null)
              {
                 _logger.LogError(" Ride cannot be added");
              }
               return Ok(data);
           }
           catch (Exception ex)
           {
                 _logger.LogError(ex.Message);

                 return null;
           }
        }

        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookedRidesDTO>> FetchRideById([FromRoute] int bookingId)
        {
            try 
            {
                var data = await _dataContext.GetRideById(bookingId);

                return Ok(_mapper.Map<BookedRidesDTO>(data.Value));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

       /* [HttpPut("{bookingId}")]
        public async Task<ActionResult<BookedRides>> UpdateRide([FromBody] BookedRides request,[FromRoute] int bookingId)
        {
            try
            {
                var data = await _dataContext.UpdateRide(request, bookingId);

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }

        [HttpDelete("{bookingId}")]
        public async Task<ActionResult<BookedRides>> DeleteRide([FromRoute] int bookingId)
        {
            try
            {
                var data = await _dataContext.DeleteRide(bookingId);

                return Ok(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                return null;
            }
        }*/
    }
}
