using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Repository
{
    public class BookedRidesRepository : GenericRepository<BookedRides>, IBookedRidesRepository
    {
        private readonly DataContext _dataContext;
        public BookedRidesRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<BookedRides>> GetRidesBookedByOfferedId(OfferedRides ride)
        {
           return await _dataContext.BookedRides.Where(bookedRide => bookedRide.OfferedId == ride.OfferedId).ToListAsync();
        }
        public async Task<List<BookedRides>> GetRidesBookedByUserId(string id)
        {
            return await _dataContext.BookedRides.Where(ride => ride.Id.ToString() == id).ToListAsync();
        }
    }
}
