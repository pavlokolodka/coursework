using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Dto
{
    public class UpdateBookingModel
    {
        [Required(ErrorMessage = "BookingID is required")]
        public string BookingID { get; set; }
   
        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime? StartDate { get; set; }

        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime? EndDate { get; set; }
    }
}
