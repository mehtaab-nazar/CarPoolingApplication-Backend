
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolingApplication.Models
{
    public class OfferedRides
    {
        [Key]
        public int OfferedId { get; set; }

        public int Id { get; set; }
        [ForeignKey("Id")]

       /* public User User { get; set; }*/

        [Required (ErrorMessage ="Starting location is mandatory")]
        public string StartingLocation { get; set; }

        [Required(ErrorMessage = "Ending location is mandatory")]
        public string EndingLocation { get; set; }

        [Required(ErrorMessage = "Date is mandatory")]
        public string Date { get; set; }

        public string TimeSlot { get; set; }

        public ICollection<string> Stops { get; set; }

        [Required(ErrorMessage = "Seats are mandatory")]
        [Range(1,3)]
        public int SeatsOffered { get; set; }

        [Required(ErrorMessage = "Cost is mandatory")]
        public int Cost { get; set; }

        public bool IsActive { get; set; } = true;

        public int AvailableSeats { get; set; }
    }
}


