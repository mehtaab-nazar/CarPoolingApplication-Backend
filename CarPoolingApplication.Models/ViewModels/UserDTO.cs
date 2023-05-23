using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Models.ViewModels
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Email Id is mandatory")]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First Name is mandatory")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
    }
}
