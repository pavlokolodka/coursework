using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Shared.Dto.Booking
{
    public class BookingModel
    {
        [Required(ErrorMessage = "PropertyID is required")]
        public Guid PropertyID { get; set; }

        [Required(ErrorMessage = "UserID is required")]
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BookingStatus Status { get; set; }

        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime StartDate { get; set; }

        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime EndDate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal TotalPrice { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "PricePerHour must be greater than 0")]
        public decimal PricePerHour { get; set; }
    }

    public enum BookingStatus
    {
        Registered,
        Finished,
    }
}
