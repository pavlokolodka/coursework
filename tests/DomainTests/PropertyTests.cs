using ReserveSpot;

namespace DomainTests
{
    [TestClass]
    public class PropertyTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            string name = "Test Property";
            string description = "Test Description";
            PropertyType type = PropertyType.Apartment;
            string location = "Test Location";
            decimal pricePerHour = 50.00m;
            int capacity = 5;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);
            string creatorID = "user123";

            Property property = new Property(name, description, type, location, pricePerHour, capacity, startDate, endDate, creatorID);

            Assert.AreEqual(name, property.Name);
            Assert.AreEqual(description, property.Description);
            Assert.AreEqual(type, property.Type);
            Assert.AreEqual(location, property.Location);
            Assert.AreEqual(pricePerHour, property.PricePerHour);
            Assert.AreEqual(capacity, property.Capacity);
            Assert.AreEqual(startDate, property.StartDate);
            Assert.AreEqual(endDate, property.EndDate);
            Assert.AreEqual(creatorID, property.UserID);
        }

        [TestMethod]
        public void Edit_UpdatesProperties()
        {
            Property property = new Property("Test Property", "Test Description", PropertyType.Apartment, "Test Location", 50.00m, 5, DateTime.Now, DateTime.Now.AddDays(7), "user123");
            PropertyDetails updateDetails = new PropertyDetails
            {
                Name = "New Name",
                Description = "New Description",
                Type = PropertyType.House,
                Location = "New Location",
                PricePerHour = 60.00m,
                Capacity = 6,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(8)
            };

            bool result = property.Edit(updateDetails);

            Assert.IsTrue(result);
            Assert.AreEqual(updateDetails.Name, property.Name);
            Assert.AreEqual(updateDetails.Description, property.Description);
            Assert.AreEqual(updateDetails.Type, property.Type);
            Assert.AreEqual(updateDetails.Location, property.Location);
            Assert.AreEqual(updateDetails.PricePerHour, property.PricePerHour);
            Assert.AreEqual(updateDetails.Capacity, property.Capacity);
            Assert.AreEqual(updateDetails.StartDate, property.StartDate);
            Assert.AreEqual(updateDetails.EndDate, property.EndDate);
        }
    }
}
