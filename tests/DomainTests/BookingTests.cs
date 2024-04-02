using ReserveSpot;
namespace DomainTests
{
    [TestClass]
    public class BookingTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);
            string userId = "user123";
            string propertyId = "property456";

            Booking booking = new Booking(startDate, endDate, userId, propertyId);

            Assert.AreEqual(startDate, booking.StartDate);
            Assert.AreEqual(endDate, booking.EndDate);
            Assert.AreEqual(userId, booking.UserID);
            Assert.AreEqual(propertyId, booking.PropertyID);
        }

        [TestMethod]
        public void Edit_UpdatesBookingDates()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);
            string userId = "user123";
            string propertyId = "property456";
            Booking booking = new Booking(startDate, endDate, userId, propertyId);
            DateTime newStartDate = startDate.AddDays(2);
            DateTime newEndDate = endDate.AddDays(2);

            bool result = booking.Edit(newStartDate, newEndDate);

            Assert.IsTrue(result);
            Assert.AreEqual(newStartDate, booking.StartDate);
            Assert.AreEqual(newEndDate, booking.EndDate);
        }

        [TestMethod]
        public void Edit_NullDates_ReturnsFalse()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);
            string userId = "user123";
            string propertyId = "property456";
            Booking booking = new Booking(startDate, endDate, userId, propertyId);

            bool result = booking.Edit(null, null);

            Assert.IsFalse(result);
            Assert.AreEqual(startDate, booking.StartDate);
            Assert.AreEqual(endDate, booking.EndDate);
        }
    }
}
