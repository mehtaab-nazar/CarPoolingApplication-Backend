using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Models.ViewModels
{
    public class BookedRidesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Starting Location is mandatory")]
        public string StartingLocation { get; set; }
        [Required(ErrorMessage = "Ending Location is mandatory")]
        public string EndingLocation { get; set; }
        [Required(ErrorMessage = "Number of seats required are mandatory")]
        [Range(1, 3)]
        public int BookedSeats { get; set; }
        [Required(ErrorMessage = "Date is mandatory")]
        public string Date { get; set; }
        public string TimeSlot { get; set; }
        public int Cost { get; set; }
        public int OfferedId { get; set; }
        public int SeatsLeft { get; set; }
    }
}
