using Bogus;
using DotNetEnv;
using ReserveSpot.Domain;
using System.ComponentModel.DataAnnotations;

namespace DomainTests
{
    [TestClass]
    public class UserTests
    {
        public Faker faker;

        [TestInitialize]
        public void Initialize() {
            Env.TraversePath().Load();
            faker = new Faker("en");
        }

        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User user = new User(email, password, firstName, lastName);

            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
            Assert.IsFalse(user.IsAdmin); 
        }

        [TestMethod]
        public void ValidateObject_ValidUser()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User user = new User(email, password, firstName, lastName);

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, true);

            Assert.IsTrue(isValid);
        }


        [TestMethod]
        public void ValidateObject_InvalidUser()
        {
            string email = faker.Lorem.Text();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User user = new User(email, password, firstName, lastName);

            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = Validator.TryValidateObject(user, context, results, true);

            Assert.IsFalse(isValid);

            var expectedErrors = new Dictionary<string, string>
            {
                { nameof(User.Email), "Invalid email address" }
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
        public void HashPassword_ReturnsTrueWhenHashPasswords()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User user = new User(email, password, firstName, lastName);   
            
            Assert.AreEqual(password, user.Password);
            user.HashPassword();
            Assert.AreNotEqual(password, user.Password);   
        }

        [TestMethod]
        public void ComparePassword_ReturnsTrueWhenPasswordsMatch()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User user = new User(email, password, firstName, lastName);
            user.HashPassword();
            bool result = user.ComparePassword(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ComparePassword_ReturnsFalseWhenPasswordsDoNotMatch()
        {
            string email = faker.Internet.Email();
            string password = faker.Internet.Password();
            string firstName = faker.Name.FirstName();
            string lastName = faker.Name.LastName();

            User user = new User(email, password, firstName, lastName);

            bool result = user.ComparePassword("wrongpassword");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidUserCode_ValidCode_ReturnsTrue()
        {
            User user = new User("test@example.com", "password", "John", "Doe");
            user.GenerateUserCode(); 

            bool isValid = user.IsValidUserCode();

            Assert.IsTrue(isValid);
        }    

    }
}