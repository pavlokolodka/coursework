using Bogus;
using ReserveSpot.Domain;

namespace DomainTests
{
    [TestClass]
    public class PropertyJSONDaoTests
    {
        public Faker faker = new Faker("en");

        [TestMethod]
        public void Save()
        {
            string name = faker.Commerce.ProductName();
            string description = faker.Lorem.Sentence();
            PropertyType type = faker.PickRandom<PropertyType>();
            string location = faker.Address.FullAddress();
            string contactPhone = faker.Phone.PhoneNumber("+380#########");
            string contactName = faker.Name.FirstName();
            decimal pricePerHour = faker.Finance.Amount(10, 1000);
            int capacity = faker.Random.Int(1, 20);
            DateTime startDate = faker.Date.Recent();
            DateTime endDate = startDate.AddDays(faker.Random.Int(1, 30));
            Guid creatorID = Guid.NewGuid();
            var dao = new PropertyJSONDao();
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);
            var createdProperty = dao.Create(property);

            Assert.AreEqual(name, createdProperty.Name);
            Assert.AreEqual(description, createdProperty.Description);
            Assert.AreEqual(type, createdProperty.Type);
            Assert.AreEqual(location, createdProperty.Location);
            Assert.AreEqual(contactPhone, createdProperty.ContactPhone);
            Assert.AreEqual(contactName, createdProperty.ContactName);
            Assert.AreEqual(pricePerHour, createdProperty.PricePerHour);
            Assert.AreEqual(capacity, createdProperty.Capacity);
            Assert.AreEqual(startDate, createdProperty.StartDate);
            Assert.AreEqual(endDate, createdProperty.EndDate);
            Assert.AreEqual(creatorID, createdProperty.UserID);

            dao.Delete(property => property.ID == createdProperty.ID);
        }

        [TestMethod]
        public void Save_Throws_Validation_Exception()
        {
            string name = faker.Commerce.ProductName();
            string description = faker.Lorem.Sentence();
            PropertyType type = faker.PickRandom<PropertyType>();
            string location = faker.Address.FullAddress();
            // invalid
            string contactPhone = faker.Phone.PhoneNumber("+000#########");
            string contactName = faker.Name.FirstName();
            // invalid
            decimal pricePerHour = faker.Finance.Amount(100000, 1000000);
            int capacity = faker.Random.Int(100, 200);
            DateTime startDate = faker.Date.Recent();
            DateTime endDate = startDate.AddDays(faker.Random.Int(1, 30));
            Guid creatorID = Guid.NewGuid();
            var dao = new PropertyJSONDao();
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);

            try
            {
                dao.Create(property);
                Assert.Fail("Expected ValidationException was not thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is System.ComponentModel.DataAnnotations.ValidationException);
            }
        }
    }
}
