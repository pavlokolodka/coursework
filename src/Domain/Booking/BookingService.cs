namespace ReserveSpot
{
    public class BookingService
    {
        private readonly IDao<Booking> bookingDao;

        // Ensure the constructor is also public
        public BookingService(IDao<Booking> dao)
        {
            bookingDao = dao;
        }

        public Booking Create(CreateBookingDto payload)
        {
            throw new NotImplementedException();
        }

        public Booking Update(UpdateBookingDto payload)
        {
            throw new NotImplementedException();

        }

        public Booking Find(FindOneBookingDto payload)
        {
            throw new NotImplementedException();
        }

        public List<Booking> FindAll(string userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DeleteBookingDto payload)
        {
            throw new NotImplementedException();
        }
    }
}
