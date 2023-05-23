using CarPoolingApplication.Data.Interfaces;
using CarPoolingApplication.Data.Repository;
using CarPoolingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingApplication.Data.Repository
{
    public class OfferedRidesRepository : GenericRepository<OfferedRides>,IOfferedRidesRepository 
    {
        private readonly DataContext _dataContext;
        public OfferedRidesRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<OfferedRides>> GetRidesOfferedByUser(string id)
        {
            return await _dataContext.OfferedRides.Where(ride => ride.Id.ToString() == id).ToListAsync();
        }
    }
}
