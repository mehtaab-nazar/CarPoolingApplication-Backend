using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Services.CustomExceptions
{
    public class RideNotFoundException:Exception
    {
            public RideNotFoundException() { }

            public RideNotFoundException(string message) : base(message)
            {

            }
    }
}
