using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    public class Booking: AbstractEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

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

        [Range(1, double.MaxValue, ErrorMessage = "PricePerNight must be greater than 0")]
        public decimal PricePerNight { get; set; }

        public Booking(string name, decimal pricePerNight, DateTime startDate, DateTime endDate, Guid userId, Guid propertyId) {
            Name = name;
            PricePerNight = pricePerNight;
            StartDate = startDate;
            EndDate = endDate;
            UserID = userId;
            PropertyID = propertyId;
            Status = BookingStatus.Registered;
            int totalDays = CountTotalDays();
            TotalPrice = CountTotalPrice(PricePerNight, totalDays);
        }
        public static decimal CountTotalPrice(decimal pricePerNight, int numberOfDays)
        {
            return pricePerNight * numberOfDays;
        }
        public void CheckBookingStatus()
        {
            if (DateTime.Now > EndDate)
            {
                Status = BookingStatus.Finished;
            }       
        }
        public int CountTotalDays()
        {
            var timeSpan = EndDate - StartDate;

            return (int)(timeSpan.TotalDays) + 1;
        }


        public void Edit(DateTime? startDate, DateTime? endDate)
        {
            if (Status == BookingStatus.Finished)
            {
                throw new InvalidOperationException("Cannot edit a finished booking");
            }

            StartDate = startDate ?? StartDate; 
            EndDate = endDate ?? EndDate; 

            int totalDays = CountTotalDays();
            TotalPrice = CountTotalPrice(PricePerNight, totalDays);
        }        
    }
}
