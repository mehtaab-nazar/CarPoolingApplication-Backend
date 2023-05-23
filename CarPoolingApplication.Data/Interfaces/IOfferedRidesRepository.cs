using CarPoolingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Data.Interfaces
{
    public interface IOfferedRidesRepository:IGenericRepository<OfferedRides>
    {
        Task<List<OfferedRides>> GetRidesOfferedByUser(string id);
    }
}
