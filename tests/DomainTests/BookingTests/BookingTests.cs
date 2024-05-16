using Bogus;
using ReserveSpot.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DomainTests
{
    [TestClass]
    public class BookingTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            DateTime startDate = new DateTime(2024, 4, 7);
            DateTime endDate = new DateTime(2024, 4, 14);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            decimal expectedTotalPrice = price * 8;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            Assert.AreEqual(name, booking.Name);
            Assert.AreEqual(startDate, booking.StartDate);
            Assert.AreEqual(endDate, booking.EndDate);
            Assert.AreEqual(price, booking.PricePerNight);
            Assert.AreEqual(expectedTotalPrice, booking.TotalPrice);
            Assert.AreEqual(userId, booking.UserID);
            Assert.AreEqual(propertyId, booking.PropertyID);
            Assert.AreEqual(BookingStatus.Registered, booking.Status);
           }

        [TestMethod]
        public void CountTotalDays()
        {
            DateTime startDate = new DateTime(2024, 4, 7);
            DateTime endDate = new DateTime(2024, 4, 14);
            int expectedTotalDays = 8;
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);


            Assert.AreEqual(expectedTotalDays, booking.CountTotalDays());
        }

        [TestMethod]
        public void CountTotalPrice_ValidInput()
        {
            decimal pricePerNight = 50.00m;
            int numberOfDays = 7;
            decimal expectedTotalPrice = 50.00m * numberOfDays;

            decimal totalPrice = Booking.CountTotalPrice(pricePerNight, numberOfDays);

            Assert.AreEqual(expectedTotalPrice, totalPrice);
        }

        [TestMethod]
        public void ValidateObject_ValidBooking()
        {
            DateTime startDate = DateTime.Now.AddSeconds(1);
            DateTime endDate = startDate.AddDays(10);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            var context = new ValidationContext(booking);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

            Assert.IsTrue(isValid);

        }

        [TestMethod]
        public void ValidateObject_InvalidBooking()
        {
            DateTime startDate = new DateTime(2024, 4, 9);
            DateTime endDate = new DateTime(2024, 4, 7);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 0M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            var context = new ValidationContext(booking);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

            Assert.IsFalse(isValid);

            var expectedErrors = new Dictionary<string, string>
            {
                { nameof(Booking.StartDate), "StartDate must be less than or equal to EndDate" },
                { nameof(Booking.EndDate), "EndDate must be greater than or equal to StartDate" },
                { nameof(Booking.TotalPrice), "TotalPrice must be greater than 0" },
                { nameof(Booking.PricePerNight), "PricePerNight must be greater than 0" }
            };

            foreach (var result in results)
            {
                foreach (var memberName in result.MemberNames)
                {
                    Assert.AreEqual(expectedErrors[memberName], result.ErrorMessage);
                }
            }
        }

        [TestMethod]
        public void Edit_UpdatesBookingDates_Valid()
        {
            DateTime startDate = DateTime.Now.AddSeconds(1);
            DateTime endDate = startDate.AddDays(10);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            DateTime newStartDate = startDate.AddDays(10);
            DateTime newEndDate = endDate.AddDays(10);
            
            booking.Edit(newStartDate, newEndDate);
            var context = new ValidationContext(booking);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

            Assert.IsTrue(isValid);
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void Edit_UpdatesBookingDates_Invalid()
        {
            DateTime startDate = new DateTime(2024, 4, 7);
            DateTime endDate = new DateTime(2024, 4, 14);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            DateTime newStartDate = new DateTime(2024, 4, 17);
            DateTime newEndDate = new DateTime(2024, 4, 10);
            
            booking.Edit(newStartDate, newEndDate);

            var context = new ValidationContext(booking);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Edit_WhenStatusIsFinished_ThrowsException()
        {
            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now.AddDays(-3);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            booking.CheckBookingStatus();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                try
                {
                    booking.Edit(DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                }
                catch (InvalidOperationException ex)
                {
                    Assert.AreEqual("Cannot edit a finished booking", ex.Message);
                    throw;
                }
            });
        }

        [TestMethod]
        public void Edit_WhenStatusIsNotFinished_UpdatesDates()
        {
            DateTime startDate = DateTime.Now.AddDays(-5);
            DateTime endDate = DateTime.Now.AddDays(-3);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            decimal price = 150.40M;
            string name = "Test booking";
            Booking booking = new Booking(name, price, startDate, endDate, userId, propertyId);

            DateTime newStartDate = DateTime.Now;
            DateTime newEndDate = DateTime.Now.AddDays(3);
            booking.Edit(newStartDate, newEndDate);
            

            Assert.AreEqual(newStartDate, booking.StartDate);
            Assert.AreEqual(newEndDate, booking.EndDate);
        }
    }
}
