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
        public BookingStatus Status { get; set; }

        [StartDateLessThanOrEqualToEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime StartDate { get; set; }

        [EndDateGreaterThanOrEqualToStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime EndDate { get; set; }

        public Booking(DateTime startDate, DateTime endDate, Guid userId, Guid propertyId) {
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

        public void Edit(DateTime? startDate, DateTime? endDate)
        {
            if (Status == BookingStatus.Finished)
            {
                throw new InvalidOperationException("Cannot edit a finished booking");
            }

            StartDate = startDate ?? StartDate; 
            EndDate = endDate ?? EndDate; 
        }        
    }
}
