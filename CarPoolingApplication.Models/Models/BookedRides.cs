
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace CarPoolingApplication.Models
{
    public class BookedRides
    {
        [Key]
        public int BookingId { get; set; }

        public int Id { get; set; }
        [ForeignKey("Id")]

       /* public User User { get; set; }*/

        public int OfferedId { get; set; }

        [Required(ErrorMessage = "Starting Location is mandatory")]
        public string StartingLocation { get; set; }

        [Required(ErrorMessage = "Ending Location is mandatory")]
        public string EndingLocation { get; set; }

        [Required(ErrorMessage = "Number of seats required are mandatory")]
        [Range(1,3)]
        public int BookedSeats { get; set; }

        public int SeatsLeft { get; set; }

        [Required(ErrorMessage = "Date is mandatory")]
        public string Date { get; set; }

        public string TimeSlot { get; set; }

        public bool IsActive { get; set; } = true;
        
        public int Cost { get; set; }
    }
}
