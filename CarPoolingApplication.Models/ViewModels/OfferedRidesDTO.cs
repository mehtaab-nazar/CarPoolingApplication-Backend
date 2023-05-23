using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingApplication.Models.ViewModels
{
    public class OfferedRidesDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Starting location is mandatory")]
        public string StartingLocation { get; set; }

        [Required(ErrorMessage = "Ending location is mandatory")]
        public string EndingLocation { get; set; }

        [Required(ErrorMessage = "Date is mandatory")]
        public string Date { get; set; }
        public string TimeSlot { get; set; }
        public ICollection<string> Stops { get; set; }

        [Required(ErrorMessage = "Seats are mandatory")]
        [Range(1, 3)]
        public int SeatsOffered { get; set; }
        [Required(ErrorMessage = "Cost is mandatory")]
        public int Cost { get; set; }
    }
}
