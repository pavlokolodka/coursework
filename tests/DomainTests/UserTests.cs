using DotNetEnv;
using ReserveSpot;

namespace DomainTests
{
    [TestClass]
    public class UserTests
    {
        [TestInitialize]
        public void Initialize() {
            Env.TraversePath().Load();
        }

        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            string email = "testuser@gmail.com";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";

            User user = new User(email, password, firstName, lastName);

            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
            Assert.IsFalse(user.IsAdmin); 
        }
        [TestMethod]
        public void HashPassword_ReturnsTrueWhenHashPasswords()
        {
            string email = "testuser@gmail.com";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";
            User user = new User(email, password, firstName, lastName);           
            Assert.AreEqual(password, user.Password);
            user.HashPassword();
            Assert.AreNotEqual(password, user.Password);   
        }


        [TestMethod]
        public void ComparePassword_ReturnsTrueWhenPasswordsMatch()
        {
            string email = "testuser@gmail.com";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";
            User user = new User(email, password, firstName, lastName);
            user.HashPassword();
            bool result = user.ComparePassword(password);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ComparePassword_ReturnsFalseWhenPasswordsDoNotMatch()
        {
            string email = "testuser@gmail.com";
            string password = "password123";
            string firstName = "John";
            string lastName = "Doe";
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