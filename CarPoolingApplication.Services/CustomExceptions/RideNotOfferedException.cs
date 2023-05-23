using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Services.CustomExceptions
{
    public class RideNotOfferedException:Exception
    {
        public RideNotOfferedException() { }

        public RideNotOfferedException(string message) : base(message)
        {

        }
    }
}
