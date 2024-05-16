using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    public class UpdateBookingDto
    {
        [Required(ErrorMessage = "BookingID is required")]
        public string BookingID { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "IsAdmin is required")]
        public bool IsAdmin { get; set; }

        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime? StartDate { get; set; }

        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime? EndDate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal PricePerNight { get; set; }
    }
}
