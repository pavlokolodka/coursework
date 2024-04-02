namespace ReserveSpot
{
    public class UpdateBookingDto
    {
        public string BookingID { get; set; }
        public string UserID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
