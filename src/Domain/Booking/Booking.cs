namespace ReserveSpot
{
    public class Booking: AbstractEntity
    {
        public string PropertyID { get; set; }
        public string UserID { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Booking(DateTime startDate, DateTime endDate, string userId, string propertyId) {
            StartDate = startDate;
            EndDate = endDate;
            UserID = userId;
            PropertyID = propertyId;
        }
        
        private void CheckBookingStatus()
        {
            throw new NotImplementedException();       
        }

        public bool Edit(DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();   
        }        
    }
}
