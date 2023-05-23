
using System.ComponentModel.DataAnnotations;

namespace CarPoolingApplication.Models.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email Id is mandatory")]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
    }
}
