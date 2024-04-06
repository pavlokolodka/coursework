using Bogus;
using ReserveSpot;
using Sprache;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DomainTests
{
    [TestClass]
    public class BookingTests
    {
        public Faker faker = new Faker("en");

        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            DateTime startDate = faker.Date.Soon();
            DateTime endDate = faker.Date.Soon(7);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();

            Booking booking = new Booking(startDate, endDate, userId, propertyId);

            Assert.AreEqual(startDate, booking.StartDate);
            Assert.AreEqual(endDate, booking.EndDate);
            Assert.AreEqual(userId, booking.UserID);
            Assert.AreEqual(propertyId, booking.PropertyID);
            Assert.AreEqual(BookingStatus.Registered, booking.Status);
        }

        [TestMethod]
        public void ValidateObject_InvalidBooking()
        {
            var faker = new Faker();
            DateTime startDate = faker.Date.Soon(2);
            DateTime endDate = faker.Date.Soon();
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
          
            Booking booking = new Booking(startDate, endDate, userId, propertyId);

            var context = new ValidationContext(booking, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

            Assert.IsFalse(isValid);

            var expectedErrors = new Dictionary<string, string>
            {
                { nameof(Booking.StartDate), "StartDate must be less than or equal to EndDate" },
                { nameof(Booking.EndDate), "EndDate must be greater than or equal to StartDate" }
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
            DateTime startDate = faker.Date.Soon();
            DateTime endDate = faker.Date.Soon(7);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            Booking booking = new Booking(startDate, endDate, userId, propertyId);
            DateTime newStartDate = faker.Date.Soon(3);
            DateTime newEndDate = faker.Date.Soon(10);

            booking.Edit(newStartDate, newEndDate);
                     
            var context = new ValidationContext(booking, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

                    
            Assert.IsTrue(isValid);
            Assert.IsTrue(results.Count == 0);          
        }

        [TestMethod]
        public void Edit_UpdatesBookingDates_Invalid()
        {
            DateTime startDate = faker.Date.Soon();
            DateTime endDate = faker.Date.Soon(7);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            Booking booking = new Booking(startDate, endDate, userId, propertyId);

            DateTime newStartDate = faker.Date.Soon(10);
            DateTime newEndDate = faker.Date.Soon();

            booking.Edit(newStartDate, newEndDate);
                     
            var context = new ValidationContext(booking, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(booking, context, results, true);

            Assert.IsFalse(isValid);
        }
    }
}
