using ReserveSpot;

namespace ReserveSpot
{
    public class CreateBookingDto
    {
        public string PropertyID { get; set; }
        public string UserID { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
