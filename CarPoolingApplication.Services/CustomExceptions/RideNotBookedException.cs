using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Services.CustomExceptions
{
    public class RideNotBookedException:Exception
    {
        public RideNotBookedException() { }

        public RideNotBookedException(string message) : base(message) { }
    }
}
