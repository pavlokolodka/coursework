using System.Data;

namespace ReserveSpot.Domain
{
    public class BookingService
    {
        private readonly IDao<Booking> bookingDao;

        public BookingService(IDao<Booking> dao)
        {
            bookingDao = dao;
        }

        public Booking Create(CreateBookingDto payload)
        {
            Booking newBooking = new(payload.TotalPrice, payload.StartDate, payload.EndDate, payload.UserID, payload.PropertyID);

            return bookingDao.Create(newBooking);
        }

        public Booking Update(UpdateBookingDto payload)
        {
            throw new NotImplementedException();

        }

        public Booking? Find(FindOneBookingDto payload)
        {
            return bookingDao.FindOne(booking => booking.UserID.Equals(payload.UserID) && booking.ID.Equals(payload.BookingID));
        }

        public List<Booking> FindAll(string userId)
        {
            return bookingDao.FindMany(booking => booking.UserID.Equals(userId));   
        }

        public bool Delete(DeleteBookingDto payload)
        {
            var booking = bookingDao.FindOne(booking => booking.ID.Equals(payload.BookingID));

            if (booking == null) {
                throw new InvalidOperationException("Booking not found");
            };

            if (booking.UserID.ToString() != payload.UserID && !payload.IsAdmin)
            {
                throw new AccessViolationException("Cannot edit another user property");
            }

            if (booking.Status == BookingStatus.Finished)
            {
                throw new DataException("Cannot delete finished booking");
            }


            bookingDao.Delete(booking => booking.ID.Equals(payload.BookingID));

            return true;
        }
    }
}
