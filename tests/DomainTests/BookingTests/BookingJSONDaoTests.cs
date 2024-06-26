﻿using ReserveSpot.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DomainTests
{
    [TestClass]
    public class BookingJSONDaoTests
    {
        [TestMethod]
        public void Create()
        {
            DateTime startDate = DateTime.Now.AddSeconds(1);
            DateTime endDate = startDate.AddDays(10);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            BookingJSONDao dao = new BookingJSONDao();
            string name = "Test booking";
            Booking newBooking = new Booking(name, 1, startDate, endDate, userId, propertyId);
            dao.Create(newBooking);

            var createdBooking = dao.FindOne(booking => booking.ID == newBooking.ID);

            Assert.AreEqual(startDate, createdBooking.StartDate);
            Assert.AreEqual(endDate, createdBooking.EndDate);
            Assert.AreEqual(userId, createdBooking.UserID);
            Assert.AreEqual(propertyId, createdBooking.PropertyID);
            Assert.AreEqual(BookingStatus.Registered, createdBooking.Status);

            dao.Delete(booking => booking.ID == newBooking.ID);
        }

        [TestMethod]
        public void Create_Throw_Validation_Exception()
        {
            DateTime startDate = new DateTime(2024, 4, 27);
            DateTime endDate = new DateTime(2024, 4, 7);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            BookingJSONDao dao = new BookingJSONDao();
            string name = "Test booking";
            Booking newBooking = new Booking(name, 0, startDate, endDate, userId, propertyId);

            try
            {
                dao.Create(newBooking);
                Assert.Fail("Expected ValidationException was not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is System.ComponentModel.DataAnnotations.ValidationException);               
            }       
        }

        [TestMethod]
        public void Update()
        {
            DateTime startDate = DateTime.Now.AddSeconds(1);
            DateTime endDate = startDate.AddDays(10);
            Guid userId = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            BookingJSONDao dao = new BookingJSONDao();
            string name = "Test booking";
            Booking newBooking = new Booking(name, 100, startDate, endDate, userId, propertyId);
            dao.Create(newBooking);

            DateTime newEndDate = endDate.AddHours(1);
            newBooking.Edit(null, newEndDate);
            var updatedBooking = dao.Update(booking => booking.ID == newBooking.ID, newBooking);

            Assert.AreEqual(startDate, updatedBooking.StartDate);
            Assert.AreEqual(newEndDate, updatedBooking.EndDate);
            Assert.AreEqual(userId, updatedBooking.UserID);
            Assert.AreEqual(propertyId, updatedBooking.PropertyID);
            Assert.AreEqual(BookingStatus.Registered, updatedBooking.Status);

            dao.Delete(booking => booking.ID == newBooking.ID);
        }
    }
}
