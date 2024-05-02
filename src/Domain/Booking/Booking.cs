using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    public class Booking: AbstractEntity
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

        [Range(1, int.MaxValue, ErrorMessage = "TotalPrice must be greater than 0")]
        public decimal TotalPrice { get; set; }


        public Booking(decimal price, DateTime startDate, DateTime endDate, Guid userId, Guid propertyId) {
            TotalPrice = price;
            StartDate = startDate;
            EndDate = endDate;
            UserID = userId;
            PropertyID = propertyId;
            Status = BookingStatus.Registered;
        }
        
        public void CheckBookingStatus()
        {
            if (DateTime.Now > EndDate)
            {
                Status = BookingStatus.Finished;
            }       
        }

        public void Edit(decimal? price, DateTime? startDate, DateTime? endDate)
        {
            if (Status == BookingStatus.Finished)
            {
                throw new InvalidOperationException("Cannot edit a finished booking");
            }

            TotalPrice = price ?? TotalPrice;
            StartDate = startDate ?? StartDate; 
            EndDate = endDate ?? EndDate; 
        }        
    }
}
