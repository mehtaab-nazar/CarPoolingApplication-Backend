using CarPoolingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Interfaces
{
    public interface IBookedRidesRepository:IGenericRepository<BookedRides>
    {
        Task<List<BookedRides>> GetRidesBookedByUserId(string id);
        Task<List<BookedRides>> GetRidesBookedByOfferedId(OfferedRides ride);
    }
}
