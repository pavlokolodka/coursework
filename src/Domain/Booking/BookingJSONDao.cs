using System.Diagnostics;

namespace ReserveSpot.Domain
{
    public class BookingJSONDao : JSONDao<Booking>, IDao<Booking>
    {
        public BookingJSONDao() : base(Path.Combine("..", "..", "..", "..", "data", "bookings.json"))
        {
        }

        public Booking Create(Booking entity)
        {
            ValidateEntity(entity);
            try
            {
                var bookings = LoadEntitites();
                bookings.Add(entity);
                SaveEntities(bookings);
                return entity;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public bool Delete(Predicate<Booking> where)
        {
            try
            {
                var bookings = LoadEntitites();
                var deletedBookings = bookings.RemoveAll(where);
                SaveEntities(bookings);
                return deletedBookings > 0;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public List<Booking> FindMany(Predicate<Booking> where)
        {
            try
            {
                var bookings = LoadEntitites();
                return bookings.FindAll(where);
            }
            catch (Exception ex)
            {
                return new List<Booking>();
            }
        }

        public Booking FindOne(Predicate<Booking> where)
        {
            try
            {
                var bookings = LoadEntitites();
                return bookings.Find(where);
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public Booking Update(Predicate<Booking> where, Booking entity)
        {
            ValidateEntity(entity);
            try
            {
                var bookings = LoadEntitites();
                var index = bookings.FindIndex(where);
                if (index != -1)
                {
                    entity.UpdatedAt = DateTime.Now;
                    bookings[index] = entity;
                    SaveEntities(bookings);
                    return entity;
                }

                throw new Exception("Cannot update nonexistent Booking");
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }
    }
}
