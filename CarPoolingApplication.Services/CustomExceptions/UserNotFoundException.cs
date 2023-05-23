
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPoolingApplication.Services.CustomExceptions
{
    public class UserNotFoundException:Exception
    {
        public UserNotFoundException() { }

        public UserNotFoundException(string message) : base(message)
        {
          
        }
    }
}
