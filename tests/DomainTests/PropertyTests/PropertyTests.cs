using Bogus;
using ReserveSpot.Domain;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DomainTests
{
    [TestClass]
    public class PropertyTests
    {
        public Faker faker = new Faker("en");

        [TestMethod]
        public void Constructor_InitializesProperties()
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

            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);

            Assert.AreEqual(name, property.Name);
            Assert.AreEqual(description, property.Description);
            Assert.AreEqual(type, property.Type);
            Assert.AreEqual(location, property.Location);
            Assert.AreEqual(contactPhone, property.ContactPhone);
            Assert.AreEqual(contactName, property.ContactName);
            Assert.AreEqual(pricePerHour, property.PricePerHour);
            Assert.AreEqual(capacity, property.Capacity);
            Assert.AreEqual(startDate, property.StartDate);
            Assert.AreEqual(endDate, property.EndDate);
            Assert.AreEqual(creatorID, property.UserID);
        }

        [TestMethod]
        public void Edit_UpdatesProperties()
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

            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);

            PropertyDetails updateDetails = new PropertyDetails
            {
                Name = faker.Commerce.ProductName(),
                Description = faker.Lorem.Sentence(),
                Type = faker.PickRandom<PropertyType>(),
                Location = faker.Address.FullAddress(),
                ContactPhone = faker.Phone.PhoneNumber("+380#########"),
                ContactName = faker.Name.FirstName(),
                PricePerHour = faker.Random.Decimal(10, 1000),
                Capacity = faker.Random.Int(1, 20),
                StartDate = faker.Date.Soon(),
                EndDate = faker.Date.Soon(7),
            };

            property.Edit(updateDetails);

            Assert.AreEqual(updateDetails.Name, property.Name);
            Assert.AreEqual(updateDetails.Description, property.Description);
            Assert.AreEqual(updateDetails.Type, property.Type);
            Assert.AreEqual(updateDetails.Location, property.Location);
            Assert.AreEqual(updateDetails.ContactPhone, property.ContactPhone);
            Assert.AreEqual(updateDetails.ContactName, property.ContactName);
            Assert.AreEqual(updateDetails.PricePerHour, property.PricePerHour);
            Assert.AreEqual(updateDetails.Capacity, property.Capacity);
            Assert.AreEqual(updateDetails.StartDate, property.StartDate);
            Assert.AreEqual(updateDetails.EndDate, property.EndDate);
        }

        [TestMethod]
        public void Edit_ValidData()
        {
            PropertyDetails updateDetail = new PropertyDetails
            {
                Name = faker.Commerce.ProductName(),
                Description = faker.Lorem.Sentence(),
                Type = PropertyType.House,
                Location = faker.Address.FullAddress(),
                ContactPhone = faker.Phone.PhoneNumber("+380#########"),
                ContactName = faker.Name.FirstName(),
                PricePerHour = faker.Finance.Amount(10, 1000),
                Capacity = faker.Random.Int(1, 20),
                StartDate = faker.Date.Soon(),
                EndDate = faker.Date.Soon(7),
            };

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

            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);

            property.Edit(updateDetail);

            var context = new ValidationContext(property, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(property, context, results, true);

            Assert.IsTrue(isValid);         
        }

        [TestMethod]
        public void Edit_ValidData_Null()
        {
            string name = faker.Commerce.ProductName();
            string description = faker.Lorem.Sentence();
            PropertyType type = faker.PickRandom<PropertyType>();
            string location = faker.Address.FullAddress();
            string contactPhone = faker.Phone.PhoneNumber("+380#########");
            string contactName = faker.Name.FirstName();
            decimal pricePerHour = faker.Finance.Amount(10, 1000);
            int capacity = faker.Random.Int(1, 20);
            DateTime startDate = DateTime.Now.AddSeconds(1);
            DateTime endDate = startDate.AddDays(faker.Random.Int(1, 30));
            Guid creatorID = Guid.NewGuid();
            Debug.WriteLine($"start {startDate}");
            Debug.WriteLine($"end {endDate}");
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);


            property.Edit(new PropertyDetails());
            Debug.WriteLine($"start {property.StartDate}");
            Debug.WriteLine($"end {property.EndDate}");


            var context = new ValidationContext(property, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(property, context, results, true);

            Assert.IsTrue(isValid);          
        }   
        
        [TestMethod]
        public void Edit_InValidData()
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

            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, creatorID);


            PropertyDetails updateDetail = new PropertyDetails
            {
                Name = faker.Commerce.ProductName(),
                Description = faker.Lorem.Sentence(),
                Type = PropertyType.House,
                Location = faker.Address.FullAddress(),
                ContactPhone = faker.Phone.PhoneNumber("+#########"),
                ContactName = faker.Name.FirstName(),
                PricePerHour = 0,
                Capacity = -100,
                StartDate = faker.Date.Soon(7),
                EndDate = faker.Date.Soon(),
            };

            property.Edit(updateDetail);

            var context = new ValidationContext(property, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(property, context, results, true);

            Assert.IsFalse(isValid);

            var expectedErrors = new Dictionary<string, string>
            {
                { nameof(Property.ContactPhone), "Invalid Ukrainian phone number" },
                { nameof(Property.PricePerHour), "PricePerHour must be greater than 0" },
                { nameof(Property.Capacity), "Capacity must be greater than 0" },
                { nameof(Property.StartDate), "StartDate must be less than or equal to EndDate" },
                { nameof(Property.EndDate), "EndDate must be greater than or equal to StartDate" }
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
        public void CountTotalPrice_ValidInput()
        {
            decimal pricePerHour = 50.00m;
            int numberOfDays = 7;
            decimal expectedTotalPrice = 50.00m * 24 * 7;

            decimal totalPrice = Property.CountTotalPrice(pricePerHour, numberOfDays);

            Assert.AreEqual(expectedTotalPrice, totalPrice);
        }
    }
}
