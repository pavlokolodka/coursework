using Bogus;
using Bogus.DataSets;
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

            string imageUrl = "https://domain/photo.jpeg";
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, imageUrl, creatorID);

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
        public void CanBookProperty()
        {
            string name = faker.Commerce.ProductName();
            string description = faker.Lorem.Sentence();
            PropertyType type = faker.PickRandom<PropertyType>();
            string location = faker.Address.FullAddress();
            string contactPhone = faker.Phone.PhoneNumber("+380#########");
            string contactName = faker.Name.FirstName();
            decimal pricePerHour = faker.Finance.Amount(10, 1000);
            int capacity = faker.Random.Int(1, 20);
            DateTime startDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 12, 31);
            Guid creatorID = Guid.NewGuid();
            string imageUrl = "https://domain/photo.jpeg";
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, imageUrl, creatorID);

            List<Booking> bookings = new List<Booking>
            {
                new Booking(name, 10, new DateTime(2024, 1, 10), new DateTime(2024, 1, 20), Guid.NewGuid(), Guid.NewGuid()),
                new Booking(name, 21, new DateTime(2024, 1, 21), new DateTime(2024, 1, 28), Guid.NewGuid(), Guid.NewGuid()),
                new Booking(name, 10, new DateTime(2024, 2, 10), new DateTime(2024, 2, 20), Guid.NewGuid(), Guid.NewGuid()),
            };

             bool canBook = property.CanBookProperty(new DateTime(2024, 1, 1), new DateTime(2024, 1, 9), bookings);
             bool canBook2 = property.CanBookProperty(new DateTime(2024, 2, 1), new DateTime(2024, 2, 8), bookings);
             bool canBook3 = property.CanBookProperty(new DateTime(2024, 4, 1), new DateTime(2024, 5, 1), bookings);
             bool cannotBook = property.CanBookProperty(new DateTime(2024, 1, 1), new DateTime(2024, 1, 10), bookings);
           bool cannotBook2 = property.CanBookProperty(new DateTime(2024, 1, 10), new DateTime(2024, 1, 20), bookings);
           bool cannotBook3 = property.CanBookProperty(new DateTime(2024, 1, 29), new DateTime(2024, 2, 10), bookings);
           bool cannotBook4 = property.CanBookProperty(new DateTime(2024, 3, 1), new DateTime(2025, 3, 1), bookings);
           bool cannotBook5 = property.CanBookProperty(new DateTime(2022, 3, 1), new DateTime(2024, 3, 1), bookings);

           Assert.IsTrue(canBook, "canBook");
            Assert.IsTrue(canBook2, "canBook2");
            Assert.IsTrue(canBook3, "canBook3");
            Assert.IsFalse(cannotBook, "cannotBook");
           Assert.IsFalse(cannotBook2, "cannotBook2");
           Assert.IsFalse(cannotBook3, "cannotBook3");
           Assert.IsFalse(cannotBook4, "cannotBook4");
           Assert.IsFalse(cannotBook5, "cannotBook5");

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

            string imageUrl = "https://domain/photo.jpeg";
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, imageUrl, creatorID);

            UpdatePropertyDto updateDetails = new UpdatePropertyDto
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
            UpdatePropertyDto updateDetail = new UpdatePropertyDto
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
                EndDate = faker.Date.Soon(10),
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

            string imageUrl = "https://domain/photo.jpeg";
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, imageUrl, creatorID);

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
            string imageUrl = "https://domain/photo.jpeg";
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, imageUrl, creatorID);


            property.Edit(new UpdatePropertyDto());
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

            string imageUrl = "https://domain/photo.jpeg";
            Property property = new Property(name, description, type, location, contactPhone, contactName, pricePerHour, capacity, startDate, endDate, imageUrl, creatorID);


            UpdatePropertyDto updateDetail = new UpdatePropertyDto
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
    }
}
