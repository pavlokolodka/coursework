namespace ReserveSpot.Domain
{
    public class DeleteBookingDto
    {
        public string BookingID { get; set; }
        public string UserID { get; set; }
        public bool IsAdmin { get; set; }
    }
}
