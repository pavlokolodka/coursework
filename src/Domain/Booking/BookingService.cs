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
            Booking newBooking = new(payload.Name, payload.PricePerHour, payload.StartDate, payload.EndDate, payload.UserID, payload.PropertyID);

            return bookingDao.Create(newBooking);
        }

        public Booking Update(UpdateBookingDto payload)
        {
            var booking = bookingDao.FindOne(booking => booking.ID.ToString() == payload.BookingID);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found");
            };

            if (booking.UserID.ToString() != payload.UserID && !payload.IsAdmin)
            {
                throw new AccessViolationException("Cannot edit another user property");
            }

            if (booking.Status == BookingStatus.Finished)
            {
                throw new DataException("Cannot update finished booking");
            }

            booking.Edit(payload.StartDate, payload.EndDate);

            return bookingDao.Update(booking => booking.ID.ToString() == payload.BookingID, booking);
        }

        public Booking? Find(FindOneBookingDto payload)
        {
            var booking = bookingDao.FindOne(booking => booking.ID.ToString() == payload.BookingID);
            if (booking != null && booking.UserID.ToString() != payload.UserID && !payload.IsAdmin)
            {
                throw new AccessViolationException("Cannot access another user property");
            }

            return booking;
        }

        public List<Booking> FindAllUserBookings(string userId)
        {
            return bookingDao.FindMany(booking => booking.UserID.ToString() == userId);   
        }

        public List<Booking> FindAllPropertyBookings(string propertyId)
        {
            return bookingDao.FindMany(booking => booking.PropertyID.ToString() == propertyId);
        }

        public bool Delete(DeleteBookingDto payload)
        {
            var booking = bookingDao.FindOne(booking => booking.ID.ToString() == payload.BookingID);

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


            bookingDao.Delete(booking => booking.ID.ToString() == payload.BookingID);

            return true;
        }
    }
}
